using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yaar.Listeners
{
    public class ConsoleListener : ListenerBase
    {
        public ConsoleListener(Pipe pipe) : base(pipe)
        {
        }

        public override void Loop()
        {
            while(true)
            {
                Console.Write("Input: ");
                var line = Console.ReadLine();
                Handle(line);
            }
        }

        public override void Output(string output)
        {
            Console.WriteLine("Yaar: {0}", output);
        }
    }
}
