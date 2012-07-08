using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yaar.Listeners;
using Yaar.Objects;

namespace Yaar.Commands
{
    class XboxCommand : ICommand
    {
        public string Handle(string input, Match match, IListener listener)
        {
            var x = XboxLive.FromGamerTag("tst9391");
            var r = x.Friends.Where(o => o.IsOnline).Aggregate("",
                                                               (current, source) =>
                                                               current + (source.Description + Environment.NewLine));
            return r.Trim();
        }

        public string Regexes { get { return "xbox"; } }
    }
}
