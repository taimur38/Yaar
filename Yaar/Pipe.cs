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
    public class Pipe
    {
        private readonly HashSet<ICommand> _commands;

        public Pipe()
        {
            _commands = new HashSet<ICommand>();
        }

        public void AddCommand(ICommand handler)
        {
            _commands.Add(handler);
        }

        public void Handle(string input, IListener listener)
        {
            if (input == null) return;
            input = input.ToLower();
            bool handled = false;

            foreach (var command in _commands)
            {
                var match = input.RegexMatch(command.Regexes);
                if (match.Success)
                {
                    listener.Output(command.Handle(input, match, listener));
                }
            }
        }
    }
}
