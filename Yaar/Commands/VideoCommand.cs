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
            var videos = Brain.Settings.Videos.SelectMany(d => d.GetFiles("*", SearchOption.AllDirectories)).ToList();
            var results = videos.Where(o => o.Name.TorrentName().ToLower().Contains(q) && o.Extension.IsVideoType() && !o.Name.Contains("sample")).OrderByDescending(o => o.LastWriteTime);
            var video = results.FirstOrDefault();
            if (video == null)
                return "I couldn't find the video";
            Process.Start(video.FullName);
            return "Playing " + video.Name.TorrentName();
        }

        public string Regexes { get { return "play(.+)"; } }
    }
}
