using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yaar.Listeners;
using Yaar.Locale;
using Yaar.Views;

namespace Yaar.Commands
{
    class NetflixCommand : ICommand
    {
        public string Handle(string input, Match match, IListener listener)
        {
            BrowserView.Create("http://www.netflix.com");
            return Speech.Yes.Parse();
        }

        public string Regexes { get { return "netflix"; } }
    }
}
