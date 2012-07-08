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
    class AlarmCommand : ICommand
    {
        public string Handle(string input, Match match, IListener listener)
        {
            ClockTicker.Instance.StopAlarm();
            Brain.Awake = true;
            return "Wake up.";
        }

        public string Regexes { get { return "alarm"; } }
    }
}
