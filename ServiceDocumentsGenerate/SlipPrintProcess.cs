using ServiceDocumentsGenerate.Entities;
using ServiceDocumentsGenerate.Entities.SlipPrint;
using ServiceDocumentsGenerate.Repositories;
using ServiceDocumentsGenerate.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

            foreach (var job in jobsList)
            {
                // Estado actual de la cotización
                var state = new SlipPrintDA().GetStateProcess(job);

                #region Codigo de prueba - Para probar cualquier cotizacion sin importar el estado
                //state = 0;
                #endregion

                if (new int[] { Convert.ToInt32(PrintEnum.State.SIN_INCIAR), Convert.ToInt32(PrintEnum.State.ERROR) }.Contains(state))
                {
                    // Formatos configurados para la cotización
                    var formatsList = new SlipPrintDA().GetFormatList(job);

                    foreach (var format in formatsList)
                    {
                        // Nro de condicionado según el ramo
                        var ncod_condicionado = new SlipPrintDA().GetCondicionado(format);

                        // Los procedures configurados para el condicionado
                        var proceduresList = new SlipPrintDA().GetProcedureList(ncod_condicionado);

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
            }
        }

        private PrintResponseVM SlipGenerate(SlipPrintVM generateQuotation)
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
