using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yaar.Listeners;
using Yaar.Utilities;

namespace Yaar.Commands
{
    class ReloadCommand : ICommand
    {
        public string Handle(string input, Match match, IListener listener)
        {
            Brain.Settings = new Settings();
            return "Reloaded";
        }

        public string Regexes { get { return "reload"; } }
    }
}
