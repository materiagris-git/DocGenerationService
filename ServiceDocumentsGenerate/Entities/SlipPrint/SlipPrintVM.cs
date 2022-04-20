using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDocumentsGenerate.Entities.SlipPrint
{
    public class SlipPrintVM
    {
        public dynamic NCOD_CONDICIONADO { get; set; }
        public List<SlipProcedureVM> PROCEDURE_LIST { get; set; }
        public dynamic SCONDICIONADO { get; set; }
        public dynamic NORDER { get; set; }
        public dynamic NID_COTIZACION { get; set; }
        public dynamic NMONTO_PLANILLA { get; set; }
        public dynamic SPLANTYPE { get; set; }
        public dynamic NBRANCH { get; set; }
        public dynamic NPRODUCT { get; set; }
        public dynamic SLOGERROR { get; set; }
        public dynamic NSTATE_DOC { get; set; }
    }
}
