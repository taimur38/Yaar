using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yaar.Listeners;

namespace Yaar.Commands
{
    class TorrentCommand : ICommand
    {
        public string Handle(string input, Match match, IListener listener)
        {
            throw new NotImplementedException();
        }

        public string Regexes { get { return "torrent (.+)"; } }
    }
}