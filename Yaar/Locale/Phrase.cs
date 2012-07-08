using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaar.Utilities;

namespace Yaar.Locale
{
    public class Phrase
    {
        private List<string> _phrases;

        public Phrase(string key, params string[] phrases)
        {
            _key = key;
            _phrases = new List<string>();
            foreach (var phrase in phrases)
            {
                Add(phrase);
            }
        }

        public void Add(string p)
        {
            _phrases.Add(p);
        }

        public string Parse(params object[] args)
        {
            var i = Constants.Random(0, _phrases.Count);
            return _phrases[i].Template(args);
        }

        private string _key;
        private string Key
        {
            get { return _key; }
        }

    }
}
