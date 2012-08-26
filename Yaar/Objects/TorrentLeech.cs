using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Yaar.Utilities;

namespace Yaar.Objects
{
    class TorrentLeech
    {
        private BrowserClient _browser;

        public TorrentLeech()
        {
            _browser = new BrowserClient("torrentleech.org");
        }

        private List<TorrentLeechEntry> Fetch(string url)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(_browser.DownloadString(url));
            var nodes = doc.DocumentNode.SelectNodes("//*[@id='torrenttable']/tbody/tr");
            if (nodes == null)
                return new List<TorrentLeechEntry>();
            return
                nodes.Select(o => new TorrentLeechEntry(o, _browser)).
                    ToList();
        }

        public List<TorrentLeechEntry> Search(string query)
        {
            var url =
                "http://www.torrentleech.org/torrents/browse/index/query/-pack+-collection+{0}/categories/10,11,14,2,26,32,17/orderby/seeders/order/desc"
                    .Template(query);
            return Fetch(url);
        }

        public List<TorrentLeechEntry> Movies()
        {
            const string url = "http://www.torrentleech.org/torrents/browse/index/query/-pack+-collection/categories/10%2C11%2C14/orderby/leechers/order/desc";
            return Fetch(url);
        }

        public List<TorrentLeechEntry> Games()
        {
            const string url = "http://www.torrentleech.org/torrents/browse/index/query/-pack+-collection/categories/17/orderby/seeders/order/desc";
            return Fetch(url);
        }

        public List<TorrentLeechEntry> Movies(string query)
        {
            var url = "http://www.torrentleech.org/torrents/browse/index/query/-pack+-collection+{0}/categories/10%2C11%2C14/orderby/leechers/order/desc".Template(query);
            return Fetch(url);
        }

        public List<TorrentLeechEntry> Games(string query)
        {
            string url = "http://www.torrentleech.org/torrents/browse/index/query/-pack+-collection+{0}/categories/17/orderby/seeders/order/desc".Template(query);
            return Fetch(url);
        }
    }

    public class TorrentLeechEntry
    {
        private readonly BrowserClient _client;
        public TorrentLeechEntry(HtmlNode node, BrowserClient client)
        {
            _client = client;
            Title = node.SelectSingleNode(".//span[@class='title']/a").InnerText;
            Friendly = Title.TorrentName();
            var size = node.SelectSingleNode(".//td[5]").InnerText;
            double number = double.Parse(size.RegexMatch(@"\d+").Value);
            if(size.Contains("GB"))
                number *= 1024;
            Size = number;
            Torrent = "http://torrentleech.org" + node.SelectSingleNode(".//td[@class='quickdownload']/a").Attributes["href"].Value;
        }

        public string Title { get; private set; }
        public string Friendly { get; private set; }
        public double Size { get; private set; }
        public string Torrent { get; private set; }

        public void Download()
        {
            var path = Friendly + ".torrent";
            _client.DownloadFile(Torrent, path);
            Process.Start(path);
        }

        public bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            if(ReferenceEquals(this, obj)) return true;
            if(obj.GetType() != this.GetType()) return false;
            return Equals((TorrentLeechEntry) obj);
        }

        protected bool Equals(TorrentLeechEntry other)
        {
            return string.Equals(Friendly, other.Friendly);
        }

        public override int GetHashCode()
        {
            return (Friendly != null) ? Friendly.GetHashCode() : 0;
        }
    }
}
