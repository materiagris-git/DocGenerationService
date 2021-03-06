namespace ServiceDocumentsGenerate
{
    partial class ProjectInstaller
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
            this.serviceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstaller1 = new System.ServiceProcess.ServiceInstaller();

            this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessInstaller1.Password = null;
            this.serviceProcessInstaller1.Username = null;

            #region Descripción Producción
            //this.serviceInstaller1.Description = "WSGeneradorDocumentos";
            //this.serviceInstaller1.DisplayName = "WSGeneradorDocumentos";
            //this.serviceInstaller1.ServiceName = "WSGeneradorDocumentos";
            //this.serviceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            //this.serviceInstaller1.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceInstaller1_AfterInstall);
            #endregion

            #region Descripción Calidad - QA
            this.serviceInstaller1.Description = "WSGeneradorDocumentosQA";
            this.serviceInstaller1.DisplayName = "WSGeneradorDocumentosQA";
            this.serviceInstaller1.ServiceName = "WSGeneradorDocumentosQA";
            this.serviceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.serviceInstaller1.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceInstaller1_AfterInstall);
            #endregion

            #region Descripción Desarrollo 
            //this.serviceInstaller1.Description = "Carga Masiva de SCTR des";
            //this.serviceInstaller1.DisplayName = "WSLoadMasivo des";
            //this.serviceInstaller1.ServiceName = "Carga Masiva de SCTR des";
            //this.serviceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            //this.serviceInstaller1.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceInstaller1_AfterInstall);
            #endregion

            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller1,
            this.serviceInstaller1});
        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller1;
        private System.ServiceProcess.ServiceInstaller serviceInstaller1;
    }
}