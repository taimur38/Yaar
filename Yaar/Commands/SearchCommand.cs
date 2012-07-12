using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yaar.Listeners;
using Yaar.Objects.Reference;
using Yaar.Runnables;

namespace Yaar.Commands
{
    class SearchCommand : ICommand
    {
        public string Handle(string input, Match match, IListener listener)
        {
            var subject = match.Groups[1].Value.Trim();
            var search = new Wolfram(subject);
            //Brain.RunnableManager.Runnable = new ProcessRunnable(search.Link);
            return search.Result;
        }

        public string Regexes { get { return "search(.+)"; } }
    }
}
