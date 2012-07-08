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
    class RatingCommand : ICommand
    {
        public string Handle(string input, Match match, IListener listener)
        {
            var query = match.Groups[1].Value.Trim();
            var imdb = new IMDB(query, true);
            var r = "{0} recieved a rating of {1}.".Template(imdb.Title, imdb.Rating);
            if (imdb.Rating > 6)
                r += Environment.NewLine + "You should probably watch it.";

            Brain.RunnableManager.Runnable = new ProcessRunnable(imdb.Page);

            return r;
        }

        public string Regexes { get { return "how was (.+)"; } }
    }
}
