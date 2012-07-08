using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yaar.Listeners
{
    public interface IListener
    {
        void Loop();
        void Output(string output);
        void Start();
        void Stop();
    }
}
