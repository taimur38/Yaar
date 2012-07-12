using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaar.Objects;
using Yaar.Views;

namespace Yaar.Tickers
{
    class XboxTicker : TickerBase
    {
        private XboxLive _old;

        public XboxTicker() : base(1.Minutes())
        {
            _old = XboxLive.FromGamerTag("tst9391");
        }

        protected override void Tick()
        {
            var xbox = XboxLive.FromGamerTag("tst9391");
            if (!xbox.Success) return;
            foreach (var source in xbox.Friends.Select(x => new { Old = _old.Friends.FirstOrDefault(o => o.GamerTag == x.GamerTag), New = x}))
            {
                if(source.Old.IsOnline != source.New.IsOnline)
                {
                    var n = source.New.IsOnline;
                    Brain.ListenerManager.CurrentListener.Output(n ? source.New.Description : source.New.GamerTag + " has signed on.");
                    TweetView.Create(source.New.Description, source.New.GamerTag, true);
                    continue;
                }

                if(source.New.IsOnline && source.Old.Presence != source.New.Presence)
                {
                    Brain.ListenerManager.CurrentListener.Output(source.New.Description);
                }
            }

            _old = xbox;
        }
    }
}
