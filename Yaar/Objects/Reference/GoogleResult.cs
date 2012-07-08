using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Yaar.Objects.Reference
{
    class GoogleResult
    {
        private JToken _json;

        public GoogleResult(JToken json)
        {
            _json = json;
        }

        public string Link { get { return (string) _json["url"];  } }
        public string Content { get { return ((string)_json["content"]).HtmlDecode().StripHtml().RemoveExtraSpaces().Trim(); } }
        public string Title { get { return (string) _json["titleNoFormatting"]; } }

        public override string ToString()
        {
            return Title + Environment.NewLine + Content;
        }

    }
}
