using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Yaar.Objects
{
    class AtomEntry
    {
        public AtomEntry(XmlNode node)
        {
            Title = node.SelectSingleNode("title").InnerText;
            Link = node.SelectSingleNode("link").Attributes["href"].Value;
            Summary = node.SelectSingleNode("summary").InnerText;
            Modified = DateTime.Parse(node.SelectSingleNode("modified").InnerText);
            Issued = DateTime.Parse(node.SelectSingleNode("issued").InnerText);
            Author = new AtomEntryAuthor(node.SelectSingleNode("author/name").InnerText, node.SelectSingleNode("author/email").InnerText);
        }

        public string Title { get; private set; }
        public string Summary { get; private set; }
        public string Link { get; private set; }
        public DateTime Modified { get; private set; }
        public DateTime Issued { get; private set; }
        public AtomEntryAuthor Author { get; private set; }
    }

    class AtomEntryAuthor
    {
        public string Name { get; private set; }
        public string Email { get; private set; }

        public AtomEntryAuthor(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
