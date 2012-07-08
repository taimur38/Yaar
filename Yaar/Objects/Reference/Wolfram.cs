using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Yaar.Objects.Reference
{
    class Wolfram
    {
        public Wolfram(string query)
        {
            var url = "http://api.wolframalpha.com/v2/query?input={0}&appid=2PWVJ9-9XEHHYT93V".Template(query);
            var doc = new XmlDocument();
            doc.LoadXml(new BrowserClient().DownloadString(url));

            Images = new List<string>();
            foreach (XmlNode node in doc.SelectNodes("//pod/subpod/img"))
            {
                Images.Add(node.Attributes["img"].Value);
            }
        }

        public List<string> Images { get; private set; } 
    }
}
