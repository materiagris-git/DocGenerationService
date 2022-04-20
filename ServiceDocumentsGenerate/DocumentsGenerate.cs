using System;
using System.ComponentModel;
using System.ServiceProcess;
using System.Timers;
using System.Configuration;

namespace ServiceDocumentsGenerate
{
    public partial class DocumentsGenerate : ServiceBase
    {
        public DocumentsGenerate()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ConfigurePolicyPrint(new ElapsedEventHandler(this.OnTimerExecutePolicyPrintProcess));
            ConfigureSlipPrint(new ElapsedEventHandler(this.OnTimerExecuteSlipPrintProcess));
        }

        protected override void OnStop()
        {
        }

        private void ConfigurePolicyPrint(ElapsedEventHandler method)
        {
            double interval = Convert.ToDouble(ConfigurationManager.AppSettings["IntervalDocPolicy"]);
            Timer timer = new Timer();
            timer.Interval = interval;
            timer.Elapsed += method;
            timer.Start();
        }

        private void ConfigureSlipPrint(ElapsedEventHandler method)
        {
            double interval = Convert.ToDouble(ConfigurationManager.AppSettings["IntervalDocQuotation"]);
            Timer timer = new Timer();
            timer.Interval = interval;
            timer.Elapsed += method;
            timer.Start();
        }

        public void OnTimerExecutePolicyPrintProcess(object sender, ElapsedEventArgs args)
        {
            //ExecutePrintProcess();
            PolicyPrintJob.RunWorkerAsync();
        }

        public void OnTimerExecuteSlipPrintProcess(object sender, ElapsedEventArgs args)
        {
            //ExecutePrintQuotationProcess();
            SlipPrintJob.RunWorkerAsync();
        }


        private void PolicyPrintJob_DoWork(object sender, DoWorkEventArgs e)
        {
            ExecutePolicyPrintProcess();
        }

        private void SlipPrintJob_DoWork(object sender, DoWorkEventArgs e)
        {
            ExecutePrintSlipProcess();
        }

        private void ExecutePolicyPrintProcess()
        {
            new PolicyPrintProcess().ExecuteProcess();
        }

        private void ExecutePrintSlipProcess()
        {
            new SlipPrintProcess().ExecuteProcess();
        }
    }
}
