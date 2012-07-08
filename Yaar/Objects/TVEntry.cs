using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Yaar.Objects
{
    class TVEntry
    {
        public string Name { get; private set; }
        public DateTime Time { get; private set; }

        public TVEntry(string name, string time)
        {
            Name = Regex.Replace(name, "-\\s[\\d\\s-x]+", "- ").Trim().Split('-')[0];
            time = time.Replace("T", "");
            Time = DateTime.ParseExact(time, "yyyyMMddhhmmss", CultureInfo.InvariantCulture).AddHours(-5);
        }
    }
}
