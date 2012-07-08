using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Yaar.Tickers
{
    class ClockTicker : TickerBase
    {
        private SoundPlayer _alarm; 

        public ClockTicker() : base(1000)
        {
            _alarm = new SoundPlayer("Sounds/alarm.wav");
            Instance = this;
        }

        public static ClockTicker Instance { get; private set; }

        protected override void Tick()
        {
            var now = DateTime.Now;
            if(now.Minute == 0 && now.Second == 0)
                Brain.ListenerManager.CurrentListener.Output("The time is " + DateTime.Now.ToShortTimeString());
            if(now.TimeOfDay.Hours == Brain.Settings.Wake.Hours && now.TimeOfDay.Minutes == Brain.Settings.Wake.Minutes && now.TimeOfDay.Seconds == 0)
                _alarm.PlayLooping();
            if(now.TimeOfDay.Hours == Brain.Settings.Sleep.Hours && now.TimeOfDay.Minutes == Brain.Settings.Sleep.Minutes && now.TimeOfDay.Seconds == 0)
            {
                Brain.ListenerManager.CurrentListener.Output("You should go to sleep soon.");
                Brain.Awake = false;
            }
        }

        public void StopAlarm()
        {
            _alarm.Stop();
        }
    }
}
