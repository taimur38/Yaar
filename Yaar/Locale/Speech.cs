using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yaar.Locale
{
    public enum Speech
    {
        Weather,
        Jacket,
        Yes,
        Email
    }

    public static class SpeechExtensions
    {
        private static readonly Language Lang;
        
        static SpeechExtensions()
        {
            Lang = new Language();
        }

        public static string Parse(this Speech phrase, params object[] args)
        {
            return Lang[phrase.ToString().ToLower()].Parse(args);
        }
    }
}
