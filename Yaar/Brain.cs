using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaar.Commands;
using Yaar.Listeners;
using Yaar.Runnables;
using Yaar.Tickers;
using Yaar.Utilities;
using Yaar.Views;

namespace Yaar
{
    public class Brain
    {
        public static ListenerManager ListenerManager { get; private set; }
        public static RunnableManager RunnableManager { get; private set; }
        public static Settings Settings { get; set; }

        public static Pipe Pipe { get; set; }

        public static bool Awake { get; set; }

        public static void Start()
        {
            var processes = Process.GetProcessesByName("yaar");
            var p = Process.GetCurrentProcess();
            foreach(var process in processes.Where(process => p.Id != process.Id))
            {
                process.Kill();
            }

            Awake = true;
            Settings = new Settings();
            Pipe = new Pipe();

            //Listeners
            ListenerManager = new ListenerManager(Pipe);

            //Runnables
            RunnableManager = new RunnableManager();

            //Handlers
            typeof(ICommand).Assembly.GetTypes()
                .Where(o => o.GetInterface(typeof(ICommand).FullName) != null && o.IsClass)
                .Select(source => (ICommand)Activator.CreateInstance(source)).ToList()
                .ForEach(o => Pipe.AddCommand(o));

            //Tickers
            typeof(TickerBase).Assembly.GetTypes()
                .Where(o => o.GetInterface(typeof(ITicker).FullName) != null && o.IsClass)
                .Where(o => o.GetConstructors().Any(x => x.GetParameters().Length == 0))
                .Select(source => (ITicker) Activator.CreateInstance(source)).ToList()
                .ForEach(o => o.Start());

            ToastView.Create("testing", "test", false);
        }
    }
}
