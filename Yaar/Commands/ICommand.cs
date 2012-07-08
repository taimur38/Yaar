using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yaar.Listeners;


namespace Yaar.Commands
{
    public interface ICommand
    {
        string Handle(string input, Match match, IListener listener);
        string Regexes { get; }
    }
}
