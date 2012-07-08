using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yaar.Listeners;
using Yaar.Tickers;

namespace Yaar.Commands
{
    class ScheduleCommand : ICommand
    {
        public string Handle(string input, Match match, IListener listener)
        {
            var task = input.RegexMatch(@".*(?=at)").Value;
            var time = input.RegexMatch(@"\d+:\d+").Value;
            var dateTime = DateTime.ParseExact(time, "HH:mm", CultureInfo.CurrentCulture);
            ScheduleTicker.Instance.AddTask(dateTime, task);
            return "Task scheduled";
        }

        public string Regexes { get { return @"(at)[^\d+](\d+:\d+)"; } }
    }
}
