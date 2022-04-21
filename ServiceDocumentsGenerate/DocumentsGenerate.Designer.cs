namespace ServiceDocumentsGenerate
{
    partial class DocumentsGenerate
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.PolicyPrintJob = new System.ComponentModel.BackgroundWorker();
            this.SlipPrintJob = new System.ComponentModel.BackgroundWorker();
            this.ReSendPE = new System.ComponentModel.BackgroundWorker();

            this.PolicyPrintJob.DoWork += new System.ComponentModel.DoWorkEventHandler(this.PolicyPrintJob_DoWork);
            this.SlipPrintJob.DoWork += new System.ComponentModel.DoWorkEventHandler(this.SlipPrintJob_DoWork);
            this.ReSendPE.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ReSendPE_DoWork);

            this.ServiceName = "Generador de Documentos";
        }

        #endregion

        private System.ComponentModel.BackgroundWorker PolicyPrintJob;
        private System.ComponentModel.BackgroundWorker SlipPrintJob;
        private System.ComponentModel.BackgroundWorker ReSendPE;
    }
}
