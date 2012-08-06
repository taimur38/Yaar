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
    class TorrentCommand : ICommand
    {
        public string Handle(string input, Match match, IListener listener)
        {
            var query = match.Groups[1].Value;
            var tl = new TorrentLeech();
            var results = tl.Search(query);
            var res = string.Empty;
            var count = 0;

            foreach(var result in results)
            {
                if (count > 7)
                    break;
                res += count++ + ": " + result.Title + ": " + result.Size + " MB" + Environment.NewLine;
            }

            Brain.Pipe.ListenNext((input2, match2, listener2) =>
                                      {
                                          var index = -1;
                                          int.TryParse(match2.Groups[1].Value, out index);
                                          if (index == -1)
                                              return "Cancelled";

                                          var selected = results[index];
                                          selected.Download();
                                          return "Begun downloading: " + selected.Friendly;
                                      }, "cancel|none|nevermind", @"download (\d+)");
                    return res;
        }

        public string Regexes { get { return "torrent (.+)"; } }
    }
}