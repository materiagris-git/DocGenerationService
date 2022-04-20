using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDocumentsGenerate.Entities
{
    public class CoverResponseVM
    {
        public List<CertificateCoverVM> coverNotSubItemList = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverMainList = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverAditionalList = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverMainNotSubItemList = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverAditionalNotSubItemList = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverLimitZeroList = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverClauseList = new List<CertificateCoverVM>();
    }
}
