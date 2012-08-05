﻿using System;
﻿using System.IO;
﻿using System.Net;
using System.Data.SQLite;


namespace Yaar.Objects
{
    internal class BrowserClient : WebClient
    {
        public BrowserClient()
        {
            Headers[HttpRequestHeader.Accept] = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            Headers[HttpRequestHeader.AcceptLanguage] = "en-US,en;q=0.8";
            Headers[HttpRequestHeader.CacheControl] = "max-age=0";
            Headers[HttpRequestHeader.UserAgent] =
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/536.5 (KHTML, like Gecko) Chrome/19.0.1084.52 Safari/536.5";
            Timeout = 60000;
        }

        public BrowserClient(string host) : this()
        {
            var result = "";
            var splits = host.Split('.');
            var domain = host;
            if (splits.Length == 3)
                domain = "." + splits[1] + "." + splits[2];
            else
                domain = "www." + host;
            try
            {
                var strPath = GetChromeCookie();
                var strDb = "Data Source=" + strPath + ";pooling=false";

                using(var conn = new SQLiteConnection(strDb))
                {
                    using(SQLiteCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText =
                            "SELECT name || '=' || value || ';' FROM cookies WHERE host_key = '{0}' OR host_key='{1}'".
                                Template(host, domain);

                        conn.Open();
                        using(SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                                result += reader.GetString(0);
                        }
                    }

                    conn.Close();
                } 
            }
            catch
            {
            }
            this.Headers[HttpRequestHeader.Cookie] = result;
            
        }

        private static string GetChromeCookie()
        {
            var path1 = @"C:\Users\Taimur\AppData\Local\Google\Chrome\User Data\Default\Cookies";
            if (File.Exists(path1))
                return path1;
            path1 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Google\Chrome\User Data\Default\cookies";
            if (File.Exists(path1))
                return path1;
            return string.Empty;
        }

        public int Timeout { get; private set; }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var result = base.GetWebRequest(address);
            result.Timeout = Timeout;
            return result;
        }

    }
}
