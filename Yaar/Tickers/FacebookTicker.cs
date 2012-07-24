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
        
        public FacebookTicker() : base(Brain.Settings.Facebook)
        {
        }

        public override void NewItem(RssItem item)
        {
            Brain.ListenerManager.CurrentListener.Output(item.Title);
            Brain.RunnableManager.Runnable = new ProcessRunnable(item.Link);
            ToastView.Create(item.Title, "facebook", item.Link, false);
        }
    }
}
