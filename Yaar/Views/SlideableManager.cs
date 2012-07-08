using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yaar.Views
{
    static class SlideableManager
    {
        private static readonly List<Slideable> Fadeables;

        static SlideableManager()
        {
            Fadeables = new List<Slideable>();
        }

        public static double AddFadeable(Slideable f)
        {
            Fadeables.Add(f);
            if (Fadeables.Count == 1)
                return 10;
            var last = Fadeables[Fadeables.Count - 2];
            return last.Top + last.Height + 10;
        }

        public static void RemoveFadeable(Slideable f)
        {
            Fadeables.Remove(f);
        }
    }
}
