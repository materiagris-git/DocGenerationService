using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDocumentsGenerate.Entities.SlipPrint
{
    public class SlipFormatVM
    {
        public dynamic NID_COTIZACION { get; set; }
        public dynamic MONTO_PLANILLA { get; set; }
        public dynamic NPRODUCT { get; set; }
        public dynamic NBRANCH { get; set; }
        public dynamic NNUM_TRABAJADORES { get; set; }
        public dynamic NCOD_CONDICIONADO { get; set; }
        public dynamic TIPO_PRODUCTO { get; set; }
    }
}
