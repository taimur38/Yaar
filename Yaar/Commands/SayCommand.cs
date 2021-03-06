﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yaar.Listeners;
using Yaar.Views;

namespace Yaar.Commands
{
    class SayCommand : ICommand
    {
        public string Handle(string input, Match match, IListener listener)
        {
            var text = match.Groups[1].Value;
            //test code
            ToastView.Create(text, "taimur38", false);
            return text;
        }

        public string Regexes { get { return "say (.+)"; } }
    }
}
