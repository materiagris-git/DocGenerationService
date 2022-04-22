using System;
using System.ServiceProcess;

namespace TestService
{
    class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main(string[] args)
        {

            Console.WriteLine("INICIO");

            try
            {
                var wsProcess = new ServiceDocumentsGenerate.DocumentsGenerate();
                //wsProcess.OnTimerExecuteSlipPrintProcess(null, null);
                wsProcess.OnTimerExecutePolicyPrintProcess(null, null);
                //wsProcess.OnTimerExecuteReSendPEProcess(null, null);

            }
            catch (Exception ex)
            {

            }

            Console.ReadLine();
        }

        private static void StopService()
        {
            ServiceController sc = new ServiceController("WSLoadMasivo");

            try
            {
                if (sc != null && sc.Status == ServiceControllerStatus.Running)
                {
                    sc.Stop();
                }
                sc.WaitForStatus(ServiceControllerStatus.Stopped);
                sc.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al detener el servicio:");
                Console.WriteLine(ex.Message);
            }

        }
        private static void StartService()
        {
            ServiceController sc = new ServiceController("WSLoadMasivo");

            try
            {
                if (sc != null && sc.Status == ServiceControllerStatus.Stopped)
                {
                    sc.Start();
                }
                sc.WaitForStatus(ServiceControllerStatus.Running);
                sc.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al arrancar el servicio:");
                Console.WriteLine(ex.Message);
            }
        }

    }
}
