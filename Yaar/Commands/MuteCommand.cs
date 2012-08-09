using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yaar.Listeners;

namespace Yaar.Commands
{
    class MuteCommand : ICommand
    {
        public string Handle(string input, Match match, IListener listener)
        {
            Brain.Muted = !Brain.Muted;
            if (Brain.Muted)
            {
                Brain.ListenerManager.Speech.Stop();
                return "Muted";
            }
            
            Brain.ListenerManager.Speech.Start();
            return "Unmuted";
            

        }

        public string Regexes { get { return "mute"; } }
    }
}
