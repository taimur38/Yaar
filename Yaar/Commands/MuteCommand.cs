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
                return "Muted";
            
            return "Unmuted";
            

        }

        public string Regexes { get { return "mute"; } }
    }
}
