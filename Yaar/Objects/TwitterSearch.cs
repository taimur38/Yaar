﻿// Json Mapping Automatically Generated By JsonToolkit Library for C#
// Diego Trinciarelli 2011
// To use this code you will need to reference Newtonsoft's Json Parser, downloadable from codeplex.
// http://json.codeplex.com/
// 
using System;
using System.Globalization;
using Yaar.Objects;
using Newtonsoft.Json;

namespace Yaar.Objects
{

    [Serializable]
    class TwitterSearch
    {

        public double Completed_in;
        public long Max_id;
        public string Max_id_str;
        public string Next_page;
        public int Page;
        public string Query;
        public string Refresh_url;
        public Result[] Results;
        public int Results_per_page;
        public int Since_id;
        public string Since_id_str;

        //Empty Constructor
        public TwitterSearch() { }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
        public static TwitterSearch FromJson(string json)
        {
            return JsonConvert.DeserializeObject<TwitterSearch>(json);
        }

        public static TwitterSearch FromUsers(params string[] users)
        {
            var url = "http://search.twitter.com/search.json?include_entities=true&q=";
            foreach (var twitter in users)
            {
                url += "from%3a{0}+OR+".Template(twitter);
            }
            var json = new BrowserClient().DownloadString(url);
            return FromJson(json);
        }
    }


    [Serializable]
    class TwitterEntityUrl
    {

        public string Url;
        public string Expanded_url;
        public string Display_url;
        public int[] Indices;

        //Empty Constructor
        public TwitterEntityUrl() { }

    }


    [Serializable]
    class User_mentions
    {

        public string Screen_name;
        public string Name;
        public int Id;
        public string Id_str;
        public int[] Indices;

        //Empty Constructor
        public User_mentions() { }

    }


    [Serializable]
    class Entities
    {

        public object[] Hashtags;
        public TwitterEntityUrl[] TwitterEntityUrls;
        public User_mentions[] User_mentions;

        //Empty Constructor
        public Entities() { }

    }


    [Serializable]
    class Metadata
    {

        public string Result_type;

        //Empty Constructor
        public Metadata() { }

    }


    [Serializable]
    class Result
    {

        public string Created_at;
        public Entities Entities;
        public string From_user;
        public int From_user_id;
        public string From_user_id_str;
        public string From_user_name;
        public object Geo;
        public long Id;
        public string Id_str;
        public string Iso_language_code;
        public Metadata Metadata;
        public string Profile_image_url;
        public string Profile_image_url_https;
        public string Source;
        public string Text;
        public string To_user;
        public int To_user_id;
        public string To_user_id_str;
        public string To_user_name;

        public DateTime Time
        {
            get
            {
                return DateTime.ParseExact(Created_at, "ddd, dd MMM yyyy HH:mm:ss zzz", CultureInfo.InvariantCulture).ToLocalTime();
            }
        }

        //Empty Constructor
        public Result() { }

    }

}
//Json Mapping End