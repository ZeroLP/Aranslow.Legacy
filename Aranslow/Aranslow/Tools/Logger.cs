using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aranslow.Tools
{
    internal class Logger
    {
        internal static void Log(string contentToLog) => System.Diagnostics.Trace.WriteLine(contentToLog);
    }
}
