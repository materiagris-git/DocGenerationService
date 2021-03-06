using Newtonsoft.Json;
using ServiceDocumentsGenerate.Entities;
using ServiceDocumentsGenerate.Entities.PolicyPrint;
using ServiceDocumentsGenerate.Repositories;
using ServiceDocumentsGenerate.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceDocumentsGenerate
{
    public class PolicyPrintProcess : GenericMethods
    {
        public void ExecuteProcess()
        {
            // Jobs para generar Documentos de Poliza
            var jobsList = new PolicyPrintDA().GetJobList();

            #region Codigo de prueba - Probar una poliza
            //var jobsList = new List<PolicyJobVM>();
            //var item = new PolicyJobVM()
            //{
            //    NIDHEADERPROC = 33390,
            //    NIDDETAILPROC = 47029,
            //    NIDFILECONFIG = 22
            //};
            //jobsList.Add(item);
            #endregion

            saveLog("Inicio", JsonConvert.SerializeObject(jobsList), "PolicyPrintProcess");

            Thread[] threads = new Thread[jobsList.Count];
            for (int i = 0; i < threads.Count(); i++)
            {
                var j = i;
                var formatsList = new PolicyPrintDA().GetFormatList(jobsList[j]);
                saveLog("formatsList", JsonConvert.SerializeObject(formatsList), "PolicyPrintProcess");
                ThreadStart starter = delegate { PolicyGenerate(formatsList, jobsList[j]); };
                threads[i] = new Thread(starter);
            }

            foreach (Thread thread in threads)
            {
                thread.Start();
            }

            //foreach (Thread thread in threads)
            //{
            //    thread.Join();
            //}

            //foreach (var job in jobsList)
            //{
            //    var formatsList = new PolicyPrintDA().GetFormatList(job);

            //    PolicyGenerate(formatsList, job);
            //}
        }

        public PrintResponseVM PolicyGenerate(List<PolicyFormatVM> formatsList, PolicyJobVM job)
        {
            var response = new PrintResponseVM() { NCODE = 0 };

            Task.Run(async () =>
            {
                Task<PrintResponseVM> TBool = PolicyGeneratePDF(formatsList, job);
            }).GetAwaiter().GetResult();

            return response;
        }

        public async Task<PrintResponseVM> PolicyGeneratePDF(List<PolicyFormatVM> formatsList, PolicyJobVM job)
        {
            var response = new PrintResponseVM() { NCODE = 0 };
            var generatePolicy = new PolicyPrintVM();
            string mensajeError = string.Empty;
            int index = 1;
            var listError = new List<int>();

            foreach (var format in formatsList)
            {
                #region Codigo de prueba - Probar un condicionado
                //if (format.NCOD_CONDICIONADO != 57)
                //{
                //    continue;
                //}
                #endregion

                var proceduresList = new PolicyPrintDA().GetProcedureList(format);

                if (proceduresList.Count > 0)
                {

                    generatePolicy = generateObjPolicy(job, format, proceduresList);
                    var msjUpdate = "Estamos en el formato " + index + " de " + formatsList.Count + " | Condicionado " + generatePolicy.SCONDICIONADO + " (" + generatePolicy.NCOD_CONDICIONADO + ")";
                    var pathsList = new SlipPrintDA().GetPathsList(generatePolicy.NCOD_CONDICIONADO, Convert.ToInt32(generatePolicy.NIDHEADERPROC));

                    generateObjUpdateState(generatePolicy, 1,
                                           Convert.ToInt32(PrintEnum.State.EN_PROCESO),
                                           msjUpdate);

                    response = new PolicyPrintDA().PolicyGeneratePDF(generatePolicy, pathsList, msjUpdate);

                    listError.Add(response.NCODE);
                    mensajeError = response.NCODE == 1 ? mensajeError + " " + response.SMESSAGE : response.NCODE == 3 ? mensajeError + " Problemas con el generador de clases COM para el componente con CLSID" : mensajeError;

                    saveLog("Poliza N° " + generatePolicy.NPOLICY + " | NIDHEADERPROC: " + generatePolicy.NIDHEADERPROC, JsonConvert.SerializeObject(response), "Response");

                    // Ver como actualizarlo al final
                    if (index == formatsList.Count &&
                        (listError.Contains(1) || listError.Contains(3)))
                    {
                        generateObjUpdateState(generatePolicy, 2,
                                               Convert.ToInt32(PrintEnum.State.ERROR),
                                               mensajeError);
                        response.SEND_PE = true;
                    }
                }

                index++;
            }

            if (!response.SEND_PE)
            {
                if (response.NCODE == 0)
                {
                    generatePolicy = generatePolicy.NIDHEADERPROC == null ? generateObjPolicy(job, new PolicyFormatVM()) : generatePolicy;
                    response = generateObjUpdateState(generatePolicy, 2,
                                                      Convert.ToInt32(PrintEnum.State.CORRECTO),
                                                      formatsList.Count > 0 ? "Se ha finalizado la generación de documentos" : "No se puede empezar con la generación de documentos, porque no se han encontrado formatos configurados");
                }
            }

            return response;
        }


    }
}
