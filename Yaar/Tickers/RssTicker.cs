using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaar.Objects;

namespace Yaar.Tickers
{
    class RssTicker : TickerBase
    {
        public string Url { get; private set; }

        public RssTicker(string url) : base(1.Minutes())
        {
        
        }

        protected override void Tick()
        {
            foreach(var item in new RssFeed(Url).New(DateTime.Now.Subtract(1.Minutes())))
            {
                NewItem(item);
            }
        }

        public virtual void NewItem(RssItem item)
        {
            Brain.ListenerManager.CurrentListener.Output(item.Title);
        }
    }
}
