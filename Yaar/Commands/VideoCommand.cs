using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Yaar.Listeners;
using Yaar.Utilities;

namespace Yaar.Commands
{
    class VideoCommand : ICommand
    {
        private IEnumerable<FileInfo> results;
        public string Handle(string input, Match match, IListener listener)
        {
            var q = match.Groups[1].Value.Trim();
            var videos = Brain.Settings.Videos.SelectMany(d => d.GetFiles("*", SearchOption.AllDirectories)).ToList();
            results = videos.Where(o => o.Name.TorrentName().ToLower().Contains(q) && o.Extension.IsVideoType() && !o.Name.Contains("sample"))
                                .OrderByDescending(o => o.LastWriteTime);

            Brain.Pipe.ListenNext(MatchEvaluator, "(.+)");

            var response = results.Aggregate(string.Empty, (current, item) => current + (item.Name.TorrentName() + ": " + item.Length/1048576 + "MB" + Environment.NewLine));

            if (results.Count() == 1)
            {
                Process.Start(results.First().FullName);
                return "Playing " + results.First().Name.TorrentName();
            }

            return response;

        }

        public string Regexes { get { return "play(.+)"; } }

        public string MatchEvaluator(string input, Match match, IListener listener)
        {
            results = results.Where(o => o.FullName.ToLower().Contains(input));

            if (!results.Any())
                return "Not Found";

            int count = results.Count();
            if(count == 1)
            {
                Process.Start(results.First().FullName);
                return "Playing " + results.First().Name.TorrentName();
            }

            Brain.Pipe.ListenNext(MatchEvaluator, "(.+)");

            return results.Aggregate(string.Empty, (current, next) => current += next.Name.TorrentName() + Environment.NewLine);
        }
    }
}
