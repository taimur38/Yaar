using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Yaar.Tickers
{
    abstract class TickerBase
    {
        private readonly Timer _timer;
        protected TimeSpan Interval;

        protected TickerBase(double interval)
        {
            _timer = new Timer(interval);
            _timer.Elapsed += (sender, args) =>
                                  {
                                      try
                                      {
                                          Tick();
                                      }

                                      catch(Exception e)
                                      {
                                          var n = this.GetType().Name;
                                          Brain.ListenerManager.CurrentListener.Output("Yaar, error at {0}: {1}".Template(n, e.Message));
                                      }
                                  };
        }

        protected TickerBase(TimeSpan interval) : this(interval.TotalMilliseconds)
        {
        }

        protected abstract void Tick();

        public void Start()
        {
            _timer.Start();
            Tick();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
