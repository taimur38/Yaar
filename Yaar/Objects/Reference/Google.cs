﻿// Json Mapping Automatically Generated By JsonToolkit Library for C#
// Diego Trinciarelli 2011
// To use this code you will need to reference Newtonsoft's Json Parser, downloadable from codeplex.
// http://json.codeplex.com/
// 
using System;
using System.Net;
using Newtonsoft.Json;

namespace Yaar.Objects.Reference
{

    [Serializable]
    class Google
    {

        public ResponseData ResponseData;
        public object ResponseDetails;
        public int ResponseStatus;

        //Empty Constructor
        public Google() { }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
        public static Google FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Google>(json);
        }
        public static Google FromQuery(string query)
        {
            var url = "http://ajax.googleapis.com/ajax/services/search/web?v=1.0&q=" + query;
            return JsonConvert.DeserializeObject<Google>(new WebClient().DownloadString(url));
        }
    }


    [Serializable]
    class Result
    {

        public string GsearchResultClass;
        public string UnescapedUrl;
        public string Url;
        public string VisibleUrl;
        public string CacheUrl;
        public string Title;
        public string TitleNoFormatting;
        public string Content;

        //Empty Constructor
        public Result() { }

    }


    [Serializable]
    class Page
    {

        public string Start;
        public int Label;

        //Empty Constructor
        public Page() { }

    }


    [Serializable]
    class Cursor
    {

        public string ResultCount;
        public Page[] Pages;
        public string EstimatedResultCount;
        public int CurrentPageIndex;
        public string MoreResultsUrl;
        public string SearchResultTime;

        //Empty Constructor
        public Cursor() { }

    }


    [Serializable]
    class ResponseData
    {

        public Result[] Results;
        public Cursor Cursor;

        //Empty Constructor
        public ResponseData() { }

    }

}
//Json Mapping End