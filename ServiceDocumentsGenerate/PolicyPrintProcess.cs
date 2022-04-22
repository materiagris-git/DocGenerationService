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

            saveLog("Inicio", JsonConvert.SerializeObject(jobsList), "PolicyPrintProcess");

            #region Codigo de prueba - Probar una poliza
            //var jobsList = new List<PolicyJobVM>();
            //var item = new PolicyJobVM()
            //{
            //    NIDHEADERPROC = 5299,
            //    NIDDETAILPROC = 12028,
            //    NIDFILECONFIG = 17
            //};
            //jobsList.Add(item);
            #endregion

            Thread[] threads = new Thread[jobsList.Count];
            for (int i = 0; i < threads.Count(); i++)
            {
                var j = i;
                var formatsList = new PolicyPrintDA().GetFormatList(jobsList[j]);
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
                //if (format.NCOD_CONDICIONADO != 7)
                //{
                //    continue;
                //}
                #endregion

                var proceduresList = new PolicyPrintDA().GetProcedureList(format);

                if (proceduresList.Count > 0)
                {
                    generatePolicy = generateObjPolicy(job, format, proceduresList);
                    var pathsList = new SlipPrintDA().GetPathsList(generatePolicy.NCOD_CONDICIONADO, Convert.ToInt32(generatePolicy.NIDHEADERPROC));

                    response = generateObjUpdateState(generatePolicy, 1,
                                                      Convert.ToInt32(PrintEnum.State.EN_PROCESO),
                                                      "Estamos en el formato " + index + " de " + formatsList.Count + " | Condicionado " + generatePolicy.SCONDICIONADO + " (" + generatePolicy.NCOD_CONDICIONADO + ")");

                    response = new PolicyPrintDA().PolicyGeneratePDF(generatePolicy, pathsList);

                    listError.Add(response.NCODE);
                    mensajeError = mensajeError + " " + response.SMESSAGE;
                    
                    // Ver como actualizarlo al final
                    if (index == formatsList.Count &&
                        listError.Contains(1))
                    {
                        response = generateObjUpdateState(generatePolicy, 2,
                                                      Convert.ToInt32(PrintEnum.State.ERROR),
                                                      mensajeError);
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
