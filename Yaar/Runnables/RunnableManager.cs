using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yaar.Runnables
{
    public class RunnableManager
    {
        public RunnableManager()
        {
        }

        public IRunnable Runnable { private get; set; }

        public bool Run()
        {
            if (Runnable == null)
                return false;
            Runnable.Run();
            return true;
        }
    }
}
