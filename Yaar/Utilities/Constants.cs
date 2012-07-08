using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yaar.Utilities
{
    class Constants
    {
        public static DirectoryInfo ConfigDirectory
        {
            get 
            { 
                var path = new DirectoryInfo(@"C:\home\bin\Yaar");
                if(!path.Exists)
                    path.Create();
                return path;
            }
        }

        public static FileInfo SettingsFile
        {
            get 
            { 
                var path = new FileInfo(ConfigDirectory.FullName + @"\settings");
                if(!path.Exists)
                    path.Create().Close();
                
                return path;
            }
        }

        private static Random _random;
        public static int Random(int min, int max)
        {
            _random = _random ?? new Random();
            return _random.Next(min, max);
        }
    }
}
