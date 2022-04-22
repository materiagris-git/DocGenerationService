using System;
using System.ComponentModel;
using System.ServiceProcess;
using System.Timers;
using System.Configuration;
using ServiceDocumentsGenerate.Util;

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
            //ConfigureReSendPE(new ElapsedEventHandler(this.OnTimerExecuteReSendPEProcess));
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

        private void ConfigureReSendPE(ElapsedEventHandler method)
        {
            double interval = Convert.ToDouble(ConfigurationManager.AppSettings["IntervalValidateHour"]);
            Timer timer = new Timer();
            timer.Interval = interval;
            timer.Elapsed += method;
            timer.Start();
        }

        public void OnTimerExecutePolicyPrintProcess(object sender, ElapsedEventArgs args)
        {
            PolicyPrintJob.RunWorkerAsync();
        }

        public void OnTimerExecuteSlipPrintProcess(object sender, ElapsedEventArgs args)
        {
            SlipPrintJob.RunWorkerAsync();
        }

        public void OnTimerExecuteReSendPEProcess(object sender, ElapsedEventArgs args)
        {
            ReSendPE.RunWorkerAsync();
        }

        private void PolicyPrintJob_DoWork(object sender, DoWorkEventArgs e)
        {
            ExecutePolicyPrintProcess();
        }

        private void SlipPrintJob_DoWork(object sender, DoWorkEventArgs e)
        {
            ExecutePrintSlipProcess();
        }

        private void ReSendPE_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime date = DateTime.Now;

            //if (date.ToString("HH:mm") == ConfigurationManager.AppSettings["ExecuteHourUpdate"].ToString())
            //{
                new ReSendPEProcess().ExecuteProcess();
            //}
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
