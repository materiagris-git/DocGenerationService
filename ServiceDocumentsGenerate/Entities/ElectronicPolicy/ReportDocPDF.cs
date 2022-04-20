using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDocumentsGenerate.Entities.ElectronicPolicy
{
    public class ReportDocPDF
    {
        public string Nombre { get; set; }
        public string Poliza { get; set; }
        public string Certificado { get; set; }
        public string SumaAsegurada { get; set; }
        public string NombrePlantilla { get; set; }
        public string Ramo { get; set; }
        public string Producto { get; set; }
        public string Prima { get; set; }
        public string DocContratante { get; set; }
    }
}
