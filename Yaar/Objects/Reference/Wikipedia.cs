using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Yaar.Views;

namespace Yaar.Objects.Reference
{
    class Wikipedia
    {
        private readonly string _description;

        public Wikipedia(string url)
        {
            var bc = new BrowserClient();
            var html = bc.DownloadString(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            try
            {
                var image = "http:" +
                            doc.DocumentNode.SelectNodes("//table").First(
                                o => o.Attributes["class"] != null && o.Attributes["class"].Value.Contains("infobox"))
                                .SelectNodes(".//a[@class='image']/img")
                                .OrderByDescending(o => int.Parse(o.Attributes["width"].Value))
                                .First().Attributes["src"].Value;
                ImageView.Create(image, url);
            }
            catch
            {
            }

            _description = "";
            foreach (var text in doc.DocumentNode.SelectNodes("//p").Select(o => o.InnerText.RemoveBrackets()))
            {
                foreach(var sentence in text.RegexSplit(@"\.\n|\. ").Where(o => !string.IsNullOrWhiteSpace(o)).Select(o => o.Trim()))
                {
                    _description += sentence + (sentence.EndsWith(".") ? "" : ".") + Environment.NewLine;
                    if (_description.Length > 300)
                        goto BreakLoops;
                }
            }

        BreakLoops:
            _description = _description.HtmlDecode().Trim();
        }

        public override string ToString()
        {
            return _description;
        }
    }
}
