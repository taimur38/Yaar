using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaar.Objects;
using Yaar.Runnables;
using Yaar.Views;

namespace Yaar.Tickers
{
    class FacebookTicker : RssTicker
    {
        private const string Url = "https://www.facebook.com/feeds/notifications.php?id=1346146357&viewer=1346146357&key=AWjoZJQSSGIdslcY&format=rss20";
        
        public FacebookTicker() : base(Url)
        {
        }

        public override void NewItem(RssItem item)
        {
            Brain.ListenerManager.CurrentListener.Output(item.Title);
            Brain.RunnableManager.Runnable = new ProcessRunnable(item.Link);
            TweetView.Create(item.Title, "facebook", item.Link, false);
        }
    }
}
