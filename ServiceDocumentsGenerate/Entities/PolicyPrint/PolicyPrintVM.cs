using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDocumentsGenerate.Entities.PolicyPrint
{
    public class PolicyPrintVM
    {
        public dynamic NCOD_CONDICIONADO { get; set; }
        public List<PolicyProcedureVM> PROCEDURE_LIST { get; set; }
        public dynamic NIDHEADERPROC { get; set; }
        public dynamic NIDDETAILPROC { get; set; }
        public dynamic SCERTYPE { get; set; }
        public dynamic NBRANCH { get; set; }
        public dynamic NPRODUCT { get; set; }
        public Nullable<long> NPOLICY { get; set; }
        public dynamic NCERTIF { get; set; }
        public dynamic NMOVEMENT { get; set; }
        public dynamic STRANSAC { get; set; }
        public dynamic SCONDICIONADO { get; set; }
        public dynamic NNTRANSAC { get; set; }
        public dynamic NORDER { get; set; }
        public dynamic NSTATE { get; set; }
        public dynamic SMESSAGE_IMP { get; set; }
        public dynamic DSTARTDATE { get; set; }
        public dynamic NTRANSAC { get; set; }
        public dynamic NOPC { get; set; }
        public dynamic NTYPE_POLICY { get; set; }
    }
}
