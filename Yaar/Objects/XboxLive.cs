using System;
using Newtonsoft.Json;

namespace Yaar.Objects
{
    [Serializable]
    class XboxLive
    {
        public bool Success;
        public string APILimit;
        public Player Player;
        public Friend[] Friends;

        //Empty Constructor
        public XboxLive(){}

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
        public static XboxLive FromJson(string json)
        {
            return JsonConvert.DeserializeObject<XboxLive>(json);
        }

        public static XboxLive FromGamerTag(string gamer)
        {
            return FromJson(new BrowserClient().DownloadString("https://xboxapi.com/friends/" + gamer));
        }
    }


    [Serializable]
     class Gamertile {

        public string Small;
        public string Large;

        //Empty Constructor
        public Gamertile(){}

    }


    [Serializable]
     class Gamerpic {

        public string Small;
        public string Large;

        //Empty Constructor
        public Gamerpic(){}

    }


    [Serializable]
     class Avatar {

        public Gamertile Gamertile;
        public Gamerpic Gamerpic;
        public string Body;

        //Empty Constructor
        public Avatar(){}

    }


    [Serializable]
     class Player {

        public string Gamertag;
        public Avatar Avatar;

        //Empty Constructor
        public Player(){}

    }


    [Serializable]
     class TitleInfo {

        public string Name;
        public object Id;

        //Empty Constructor
        public TitleInfo(){}

    }


    [Serializable]
     class Friend {

        public string GamerTag;
        public string GamerTileUrl;
        public string LargeGamerTileUrl;
        public int GamerScore;
        public bool IsOnline;
        public DateTime LastSeen;
        public string Presence;
        public TitleInfo TitleInfo;
        public string RichPresence;

        //Empty Constructor
        public Friend(){}

        public string Description
        {
            get
            {
                var p = char.ToLower(Presence[0]) + Presence.Substring(1);
                return GamerTag.UppercaseFirst() + " is " + p + (RichPresence.IsBlank() ? "" : ": " + RichPresence); 
            }
        }
    }
}
