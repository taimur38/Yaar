using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Yaar.Views
{
    static class SlideableManager
    {
        private static readonly List<Slideable> Slideables;
        private static bool _dismissingAll = false;

        static SlideableManager()
        {
            Slideables = new List<Slideable>();
        }

        public static double AddFadeable(Slideable f)
        {
            Slideables.Add(f);
            if (Slideables.Count == 1)
                return 10;
            var last = Slideables[Slideables.Count - 2];
            return last.Top + last.Height + 10;
        }

        public static void RemoveFadeable(Slideable f)
        {
            Slideables.Remove(f);
            if (_dismissingAll) return;
            foreach(var view in Slideables.Where(o => o.Top > f.Top))
                view.SlideTo(view.Left, view.Top - f.Height - 10);
        }

        public static void DismissAll()
        {
            _dismissingAll = true;
            while(Slideables.Count > 0)
            {
                Slideables.First().SlideOut();
            }
            _dismissingAll = false;
        }

        public static Slideable TopVisibleView()
        {
            return Slideables.FirstOrDefault(o => o.Top == 10);
        }

        public static void ScrollUp()
        {
            var top = Slideables.MaxBy(o => o.Top);

            foreach (var slideable in Slideables)
                slideable.SlideTo(slideable.Left, slideable.Top - top.Height);
        }

        public static void ScrollDown()
        {
            var top = Slideables.MaxBy(o => o.Top);

            foreach(var slideable in Slideables)
                slideable.SlideTo(slideable.Left, slideable.Top + top.Height);
        }
    }
}
