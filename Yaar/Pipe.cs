using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yaar.Listeners;
using Yaar.Commands;

namespace Yaar
{
    public delegate string ResponseHandler(string input, Match match, IListener listener);

    public class Pipe
    {
        private readonly HashSet<ICommand> _commands;
        private DynamicCommand _next;

        public Pipe()
        {
            _commands = new HashSet<ICommand>();
        }

        private class DynamicCommand
        {
            public ResponseHandler Function { get; set; }
            public List<string> Regexes { get; set; }
 
            public bool Execute(string input, IListener listener )
            {
                foreach(var regex in Regexes)
                {
                    var match = input.RegexMatch(regex);
                    if (!match.Success)
                        continue;
                    try
                    {
                        listener.Output(Function(input, match, listener));
                    }
                    catch(Exception e)
                    {
                        listener.Output("Error: " + e.Message);
                    }
                    return true;
                }
                return false;
            }
        }

        public void AddCommand(ICommand handler)
        {
            _commands.Add(handler);
        }

        public void ListenNext(ResponseHandler handler, string regex, params string[] regexes)
        {
            var cmd = new DynamicCommand()
                {
                    Function = handler,
                    Regexes = regexes.Select(o => o.ToLower()).ToList(),
                };
            cmd.Regexes.Add(regex);
            _next = cmd;
        }

        public void Handle(string input, IListener listener)
        {
            if (input == null) return;
            input = input.ToLower();

            if (_next != null && _next.Execute(input, listener))
            {
                _next = null;
            }

            foreach (var command in _commands)
            {
                var match = input.RegexMatch(command.Regexes);
                if (match.Success)
                {
                    listener.Output(command.Handle(input, match, listener));
                }
            }

            //_next = null;
        }
    }
}
