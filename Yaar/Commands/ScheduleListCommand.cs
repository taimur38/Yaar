using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yaar.Listeners;
using Yaar.Tickers;

namespace Yaar.Commands
{
    class ScheduleListCommand : ICommand
    {
        public string Handle(string input, Match match, IListener listener)
        {
            var output = ScheduleTicker.Instance.Tasks
                .Where(o => o.DateTime < DateTime.Now.AddDays(2))
                .Aggregate("", (current, task) => current + (task.Description + " at " + task.DateTime.ToShortTimeString() + Environment.NewLine));
            return output.Trim();
        }

        public string Regexes { get { return "schedule"; } }
    }
}
