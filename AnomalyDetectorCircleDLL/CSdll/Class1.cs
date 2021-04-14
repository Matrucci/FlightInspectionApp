using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSdll
{
    public class Class1
    {
        [DllImport("AnomalyDetectorCircleDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void getAnomaly(string CSVLearnFileName, string CSVTestFileName, string txtFileName);
    }
}





