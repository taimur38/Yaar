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
        public string Handle(string input, Match match, IListener listener)
        {
            var q = match.Groups[1].Value.Trim();
            //q = "*{0}".Template(q.RegexReplace(@"[\s]", "*"));
            var videos = Brain.Settings.Videos.SelectMany(d => d.GetFiles("*", SearchOption.AllDirectories)).ToList();
            var results = videos.Where(o => o.Name.TorrentName().ToLower().Contains(q)).OrderByDescending(o => o.Length);
            var video = results.FirstOrDefault();
            if (video == null)
                return "I couldn't find the video";
            Process.Start(video.FullName);
            var name = video.Name.Replace(".", " ").Trim().RegexRemove(video.Extension);
            name = name.RegexReplace(@"s(\d+)", "Season $1 ");
            name = name.RegexReplace(@"e(\d+)", "Episode $1 ");
            name = name.Trim().RemoveExtraSpaces();
            name = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());
            return "Playing " + name;
        }

        public string Regexes { get { return "play(.+)"; } }
    }
}
