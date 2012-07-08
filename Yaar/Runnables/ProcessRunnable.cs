using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yaar.Runnables
{
    class ProcessRunnable : IRunnable
    {
        private ProcessStartInfo _info;

        public ProcessRunnable(string process) : this(new ProcessStartInfo(process))
        {
        }

        public ProcessRunnable(string process, string args) : this(new ProcessStartInfo(process, args))
        {
        }

        public ProcessRunnable(ProcessStartInfo info)
        {
            _info = info;
        }

        public void Run()
        {
            Process.Start(_info);
        }
    }
}
