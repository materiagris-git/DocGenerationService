using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDocumentsGenerate.Entities.ElectronicPolicy
{
    public class ReportUserPDF
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string DNI { get; set; }
        public List<Documento> Documentos { get; set; }
    }

    public class Documento
    {
        public string nombre { get; set; }
    }
}
