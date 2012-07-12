using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yaar.Listeners;
using Yaar.Objects.Reference;
using Yaar.Views;

namespace Yaar.Commands
{
    class WolframCommand : ICommand
    {
        public string Handle(string input, Match match, IListener listener)
        {
            var w = new Wolfram(input);
            return w.Result;
        }

        public string Regexes { get { return "(what is|when|how many|whats) (.+)"; } }
    }
}
