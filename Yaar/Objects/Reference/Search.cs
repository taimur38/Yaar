using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jarvis.Objects.Reference;
using Newtonsoft.Json.Linq;

namespace Yaar.Objects.Reference
{
    class Search
    {
        public string Link { get; private set; }
        public string Description { get; private set; }

        public Search(string query)
        {
            var google = Google.FromQuery(query);
            var results = google.ResponseData.Results;
            var lucky = results[0];

            Link = lucky.Url;
            Description = lucky.Content.StripHtml();

            var result = results.FirstOrDefault(o => o.Url.ToLower().Contains("imdb"));
            if(result != null)
            {
                var imdb = IMDB.FromQuery(query);
                Description =
                    "{0} is a movie rated {1} by IMDB. It came out in {2} and the synopsis reads: ".Template(
                        imdb.Title, imdb.ImdbRating, imdb.Released, imdb.Plot);
                Link = imdb.Page;
                return;
            }

            result = results.FirstOrDefault(o => o.Url.ToLower().Contains("wikipedia"));
            if(result != null)
            {
                Description = new Wikipedia(result.Url) + Environment.NewLine;
                Link = result.Url;
            }
        }
    }
}
