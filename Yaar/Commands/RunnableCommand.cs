using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yaar.Listeners;
using Yaar.Locale;

namespace Yaar.Commands
{
    class RunnableCommand : ICommand
    {
        public string Handle(string input, Match match, IListener listener)
        {
            return Brain.RunnableManager.Run() ? Speech.Yes.Parse() : "";
        }

        public string Regexes { get { return @"\bmore\b|\brun\b\|\bopen\b"; } }
    }
}
