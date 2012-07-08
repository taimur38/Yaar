using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaar.Objects;
using Yaar.Utilities;

namespace Yaar.Tickers
{
    class ScheduleTicker : TickerBase
    {
        private string _path = Constants.ConfigDirectory.FullName + "/schedule.txt";
        public ScheduleTicker() : base(1.Minutes())
        {
            Tasks = new HashSet<ScheduledTask>();
            Instance = this;
            File.ReadAllLines(_path).Select(line => line.Split('=')).ToList()
                .ForEach(splits => AddTask(DateTime.Parse(splits[0]), splits[1]));
        }

        public static ScheduleTicker Instance { get; private set; }
        public HashSet<ScheduledTask> Tasks { get; private set; }

        private object _lock = new object();
        protected override void Tick()
        {
            lock(_lock)
            {
                var now = DateTime.Now;

                var expired = Tasks.Where(o => o.DateTime <= now);
                foreach (var task in expired)
                {
                    Brain.ListenerManager.CurrentListener.Output(task.Description);
                }
                Tasks = new HashSet<ScheduledTask>(Tasks.Where(o => o.DateTime > now));

                var sw = new StreamWriter(_path);
                foreach (var task in Tasks)
                {
                    sw.WriteLine("{0}={1}", task.DateTime, task.Description);
                }
                sw.Close();
            }
        }

        public void AddTask(DateTime dateTime, string desc)
        {
            lock(_lock)
            {
                Tasks.Add(new ScheduledTask(dateTime, desc));
            }
        }
    }
}
