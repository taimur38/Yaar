using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yaar.Views
{
    static class FadeableManager
    {
        private static readonly List<Fadeable> Fadeables;

        static FadeableManager()
        {
            Fadeables = new List<Fadeable>();
        }

        public static double AddFadeable(Fadeable f)
        {
            Fadeables.Add(f);
            if (Fadeables.Count == 1)
                return 10;
            var last = Fadeables[Fadeables.Count - 2];
            return last.Top + last.Height + 10;
        }

        public static void RemoveFadeable(Fadeable f)
        {
            Fadeables.Remove(f);
        }
    }
}
