using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yaar.Runnables
{
    class ActionRunnable : IRunnable
    {
        private readonly Action _action;

        private ActionRunnable(Action action)
        {
            _action = action;
        }

        public void Run()
        {
            _action();
        }
    }
}
