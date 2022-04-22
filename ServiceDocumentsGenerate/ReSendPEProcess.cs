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
    public class ReSendPEProcess : GenericMethods
    {
        public void ExecuteProcess()
        {
            var jobsList = new ReSendPeDA().GetJobList();

            saveLog("Inicio", JsonConvert.SerializeObject(jobsList), "ReSendPEProcess");

            #region Codigo de prueba - Probar una poliza
            //var jobsList = new List<PolicyJobVM>();
            //var item = new PolicyJobVM()
            //{
            //    NIDHEADERPROC = 30481,
            //    NIDDETAILPROC = 45815,
            //    NIDFILECONFIG = 18
            //};
            //jobsList.Add(item);
            #endregion


            Thread[] threads = new Thread[jobsList.Count];
            for (int i = 0; i < threads.Count(); i++)
            {
                var j = i;
                var formatsList = new PolicyPrintDA().GetFormatList(jobsList[j]);
                if (formatsList.Count() > 0)
                {
                    ThreadStart starter = delegate { PolicyGenerate(formatsList, jobsList[j]); };
                    threads[i] = new Thread(starter);
                }
            }

            foreach (Thread thread in threads)
            {
                thread.Start();
            }

            //foreach (var job in jobsList)
            //{
            //    var formatsList = new ReSendPeDA().GetFormatList(job);

            //    if (formatsList.Count() > 0)
            //    {
            //        PolicyGenerate(formatsList, job);
            //    }
            //}
        }

        public PrintResponseVM PolicyGenerate(List<PolicyFormatVM> formatsList, PolicyJobVM job)
        {
            var response = new PrintResponseVM() { NCODE = 0 };

            var generatePolicy = generateObjPolicy(job, formatsList[0]);

            if (new string[] { "A", "E", "R", "I" }.ToList().Contains(generatePolicy.NTRANSAC))
            {
                var formatOrderVM = new PolicyGenericDA().GetFormatLastOrder(generatePolicy);

                formatOrderVM[0].SNAME_ATTACHED_DOC = generatePolicy.NTRANSAC == "A" ? "00_CONSTANCIA_EXCLUSION.pdf": formatOrderVM[0].SNAME_ATTACHED_DOC;
                response = new PolicyGenericDA().SendElectronicPolicy(generatePolicy, formatOrderVM[0].SNAME_ATTACHED_DOC);
            }

            return response;
        }
    }
}
