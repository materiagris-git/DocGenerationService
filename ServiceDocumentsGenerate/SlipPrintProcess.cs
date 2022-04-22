using Newtonsoft.Json;
using ServiceDocumentsGenerate.Entities;
using ServiceDocumentsGenerate.Entities.SlipPrint;
using ServiceDocumentsGenerate.Repositories;
using ServiceDocumentsGenerate.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceDocumentsGenerate
{
    public class SlipPrintProcess : GenericMethods
    {
        public void ExecuteProcess()
        {
            // Jobs para generar Slip
            var jobsList = new SlipPrintDA().GetJobList();

            #region Codigo de prueba - Probar una cotizacion
            //var jobsList = new List<SlipJobVM>();
            //var item = new SlipJobVM()
            //{
            //    NBRANCH = 73,
            //    NPRODUCT = 1,
            //    NID_COTIZACION = 26414
            //};
            //jobsList.Add(item);
            #endregion

            saveLog("Inicio", JsonConvert.SerializeObject(jobsList), "SlipPrintProcess");

            Thread[] threads = new Thread[jobsList.Count];
            for (int i = 0; i < threads.Count(); i++)
            {
                var j = i;
                var state = new SlipPrintDA().GetStateProcess(jobsList[j]);

                #region Codigo de prueba - Para probar cualquier cotizacion sin importar el estado
                //state = 0;
                #endregion

                if (new int[] { Convert.ToInt32(PrintEnum.State.SIN_INCIAR), Convert.ToInt32(PrintEnum.State.ERROR) }.Contains(state))
                {
                    var formatsList = new SlipPrintDA().GetFormatList(jobsList[j]);
                    ThreadStart starter = delegate { GenerateProcess(formatsList); };
                    threads[i] = new Thread(starter);
                }
            }

            foreach (Thread thread in threads)
            {
                thread.Start();
            }

            //foreach (var job in jobsList)
            //{
            //    // Estado actual de la cotización
            //    var state = new SlipPrintDA().GetStateProcess(job);

            //    #region Codigo de prueba - Para probar cualquier cotizacion sin importar el estado
            //    //state = 0;
            //    #endregion

            //    if (new int[] { Convert.ToInt32(PrintEnum.State.SIN_INCIAR), Convert.ToInt32(PrintEnum.State.ERROR) }.Contains(state))
            //    {
            //        // Formatos configurados para la cotización

            //    }
            //}
        }

        public void GenerateProcess(List<SlipFormatVM> formatsList)
        {
            foreach (var format in formatsList)
            {
                // Nro de condicionado según el ramo
                format.NCOD_CONDICIONADO = new SlipPrintDA().GetCondicionado(format);

                // Los procedures configurados para el condicionado
                var proceduresList = new SlipPrintDA().GetProcedureList(format.NCOD_CONDICIONADO);

                if (proceduresList != null && proceduresList.Count > 0)
                {
                    var generateQuotation = new SlipPrintVM()
                    {
                        NCOD_CONDICIONADO = format.NCOD_CONDICIONADO,
                        PROCEDURE_LIST = proceduresList,
                        NID_COTIZACION = format.NID_COTIZACION,
                        NBRANCH = format.NBRANCH,
                        NPRODUCT = format.NPRODUCT
                    };

                    var slipPrint = SlipGenerate(generateQuotation);
                }
            }
        }

        public PrintResponseVM SlipGenerate(SlipPrintVM generateQuotation)
        {
            var response = new PrintResponseVM();

            try
            {
                var printPathsList = new SlipPrintDA().GetPathsList(generateQuotation.NCOD_CONDICIONADO);
                generateQuotation.NSTATE_DOC = Convert.ToInt32(PrintEnum.State.EN_PROCESO);
                new SlipPrintDA().UpdateStateSlip(generateQuotation);

                Task.Run(async () =>
                {
                    Task<PrintResponseVM> TBool = SlipGeneratePDF(generateQuotation, printPathsList[0]);

                }).GetAwaiter().GetResult();

            }
            catch (Exception ex)
            {
                response.NCODE = 1;
                response.SMESSAGE = ex.ToString();
            }

            return response;
        }

        public async Task<PrintResponseVM> SlipGeneratePDF(SlipPrintVM generateQuotation, SlipPathVM printPaths)
        {
            var response = new PrintResponseVM() { NCODE = 0 };

            response = new SlipPrintDA().SlipPDF(generateQuotation, printPaths);

            if (response.NCODE == 0 && (int)generateQuotation.NBRANCH == Convert.ToInt32(GetValueConfig("vidaLeyBranch")))
            {
                var responseCCE = SlipCCEGeneratePDF(generateQuotation);

                if (!string.IsNullOrEmpty(responseCCE.PATH_PDF))
                {
                    response.PATH_PDF = response.PATH_PDF + "\\01_Estado_Cuenta.pdf";
                    File.Move(responseCCE.PATH_PDF, response.PATH_PDF);
                }
            }

            return response;
        }

        public PrintResponseVM SlipCCEGeneratePDF(SlipPrintVM generateQuotation)
        {
            var response = new PrintResponseVM() { NCODE = 0 };

            var responseDebt = new SlipPrintDA().ValidateDebt(generateQuotation);

            if (responseDebt.P_COD_ERR != 0)
            {
                var printPaths = new SlipPrintDA().GetPathsList(27);
                response = new SlipPrintDA().SlipCCEPDF(responseDebt, printPaths);
            }

            return response;
        }


    }
}
