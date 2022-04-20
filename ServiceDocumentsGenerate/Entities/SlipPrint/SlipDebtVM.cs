using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDocumentsGenerate.Entities.SlipPrint
{
    public class SlipDebtVM
    {
        public string P_SMESSAGE { get; set; }
        public int P_COD_ERR { get; set; }
        public string P_SCLIENT { get; set; }
        public int P_NPRODUCT { get; set; }
        public int P_NBRANCH { get; set; }
        public string AMOUNT { get; set; }
        public int EXTERNAL { get; set; }
        public int COD_CONDICIONADO { get; set; }
    }
}
