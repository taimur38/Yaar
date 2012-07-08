using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Yaar.Objects
{
    class RssFeed
    {
        public string Url { get; private set; }
        public string Raw { get; private set; }
        public XmlDocument Xml { get; private set; }
        public List<RssItem> Items { get; private set; }
        public string Title { get; private set; }

        public List<RssItem> New(DateTime min)
        {
            return Items.Where(o => o.PubDate.IsFuture(min)).ToList();
        }

        public RssFeed(string url)
        {
            Url = url;
            Raw = new BrowserClient().DownloadString(url);
            Xml = new XmlDocument();
            Xml.LoadXml(Raw);

            Title = Xml.SelectSingleNode("//rss/channel/title").InnerText;
            Items = new List<RssItem>();
            foreach (XmlNode node in Xml.SelectNodes("//item"))
            {
                Items.Add(new RssItem(node));
            }
        }
    }

    internal class RssItem
    {
        public XmlNode Node { get; private set; }
        public string Guid { get; private set; }
        public string Title { get; private set; }
        public string Link { get; private set; }
        public string Description { get; private set; }
        public DateTime PubDate { get; private set; }

        public RssItem(XmlNode node)
        {
            Node = node;
            Title = node.SelectSingleNode("title").InnerText;
            Guid = Node.SelectSingleNode("guid").InnerText;
            Link = Node.SelectSingleNode("link").InnerText;
            Description = Node.SelectSingleNode("description").InnerText;
            PubDate = DateTime.Parse(Node.SelectSingleNode("pubDate").InnerText);
        }
    }
}
