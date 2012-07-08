﻿using System;
using System.Net;

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

        public int Timeout { get; private set; }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var result = base.GetWebRequest(address);
            result.Timeout = Timeout;
            return result;
        }

    }
}
