using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Yaar.Objects.Reference
{
    class Search
    {
        public string Link { get; private set; }
        public string Description { get; private set; }

        public Search(string query)
        {
            var url = "http://ajax.googleapis.com/ajax/services/search/web?v=1.0&q={0}".Template(query);
            var json = JToken.Parse(new BrowserClient().DownloadString(url));
            var results = json["responseData"]["results"].Select(o => new GoogleResult(o)).ToList();
            var result = results.FirstOrDefault(o => o.Link.ToLower().Contains("imdb"));
            if(result != null)
            {
                Description += new IMDB(result.Link) + Environment.NewLine;
                Link = result.Link;
            }

            result = results.FirstOrDefault(o => o.Link.ToLower().Contains("wikipedia"));
            if(result != null)
            {
                Description += new Wikipedia(result.Link) + Environment.NewLine;
                Link = result.Link;
            }

            if(string.IsNullOrEmpty(Description))
            {
                Description = results[0].ToString().StripHtml().RemoveExtraSpaces().Trim();
                Link = results[0].Link;
            }
        }
    }
}
