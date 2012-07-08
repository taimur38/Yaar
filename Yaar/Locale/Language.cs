using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaar.Utilities;

namespace Yaar.Locale
{
    class Language
    {
        private FileInfo _filePath;
        private Dictionary<string, Phrase> _map;

        public Language()
        {
            _filePath = new FileInfo(Constants.ConfigDirectory + @"\english.lang");
            if(!_filePath.Exists)
                _filePath.Create().Close();
            _map = new Dictionary<string, Phrase>();
            var lines = File.ReadAllLines(_filePath.FullName).Where(x => !x.IsBlank());
            foreach (var splits in lines.Select(line => line.Split('=')).Where(splits => splits.Length ==2))
            {
                var key = splits[0].ToLower();
                Phrase phrase;
                if (!_map.TryGetValue(key, out phrase))
                    phrase = Create(key);
                phrase.Add(splits[1]);
            }
        }

        private Phrase Create(string key)
        {
            var phrase = new Phrase(key);
            _map[key] = phrase;
            return phrase;
        }

        public Phrase this[string key]
        {
            get { return _map[key]; }
        }
    }
}
