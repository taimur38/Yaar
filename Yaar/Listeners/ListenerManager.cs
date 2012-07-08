using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yaar.Listeners
{
    public class ListenerManager
    {
        public  ListenerManager(Pipe pipe)
        {
            Console = new ConsoleListener(pipe);
            //Console.Start();

            Irc = new IRCListener(pipe);
            Irc.Start();

            Speech = new SpeechListener(pipe);
            Speech.Start();

            CurrentListener = Irc;
        }

        public IListener CurrentListener { get; private set; }
        public ConsoleListener Console { get; private set; }
        public IRCListener Irc { get; private set; }
        public SpeechListener Speech { get; private set; }
    }
}
