using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yaar.Utilities
{
    static class StringExtensions
    {
        public static string TorrentName(this string name)
        {
            var input = name;
            var splits = input.RegexSplit(@"[\.\s]");
            var output = "";
            foreach (var split in splits)
            {
                if (split.ToLower().IsRegexMatch(@"(-|dvdrip|720p|internal|x264|hdtv|proper|\d\d\d\d|unrated)")) break;
                output += split.UppercaseFirst() + " ";
            }
            output = output.RegexRemove(@"\d\d\d\d \d\d \d\d");
            output = output.RemoveExtraSpaces();
            return output;
        }
    }
}
