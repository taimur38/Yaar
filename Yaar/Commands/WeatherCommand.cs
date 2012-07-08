using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yaar.Listeners;
using Yaar.Objects.Reference;

namespace Yaar.Commands
{
    class WeatherCommand : ICommand
    {
        public string Handle(string input, Match match, IListener listener)
        {
            var weather = new Weather();
            return weather.ToString();
        }

        public string Regexes { get { return "weather"; } }
    }
}
