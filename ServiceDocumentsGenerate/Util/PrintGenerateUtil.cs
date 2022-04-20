using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDocumentsGenerate.Util
{
    public class PrintGenerateUtil
    {
        private int correlativeNumber;

        public int getCorrelativeNumber()
        {
            correlativeNumber++;
            return correlativeNumber;
        }
    }
}
