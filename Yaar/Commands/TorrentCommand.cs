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
            List<TorrentLeechEntry> results;

            Brain.Pipe.ListenNext((input1, match1, listener1) =>
                {
                    if (input1.Contains("movie"))
                        results = tl.Movies(query);
                    else if (input1.Contains("game"))
                        results = tl.Games(query);
                    else
                        results = new List<TorrentLeechEntry>();
                    
                    Brain.Pipe.ListenNext((input2, match2, listener2) => "", "cancel|none|never mind", results.Select(o => o.Title.ToLower()).ToArray());

                    return results.Aggregate(string.Empty, (current, result) => current + (result.Title + " " + result.Size + Environment.NewLine));

                }, "movie", "game");

            return "How would you like me to refine that search, sir";
        }

        public string Regexes { get { return "torrent (.+)"; } }
    }
}