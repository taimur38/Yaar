using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yaar.Listeners;

namespace Yaar.Commands
{
    class CloseCommand : ICommand
    {
        public string Handle(string input, Match match, IListener listener)
        {
            var process = match.Groups[1].Value.ToLower();
            var list = Process.GetProcesses().Where(o => o.ProcessName.ToLower().Contains(process)).ToList();
            var closed = new HashSet<string>();
            foreach (var p in list)
            {
                try
                {
                    p.Kill();
                    closed.Add(p.ProcessName);
                }
                catch(Exception)
                {
                }
            }

            return closed.Aggregate("", (current, close) => current + "I've closed " + close + Environment.NewLine);
        }

        public string Regexes { get { return @"close (.+)"; } }
    }
}
