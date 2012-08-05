using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Meebey.SmartIrc4net;

namespace Yaar.Listeners
{
    public class IRCListener : ListenerBase
    {
        private IrcClient _client;

        public IRCListener(Pipe pipe)
            : base(pipe)
        {
        }

        public override void Loop()
        {
           while(true)
           {
               try
               {
                   Setup();
                   _client.Listen();
               }
               catch (Exception e)
               {
                  
               }

               Thread.Sleep(5.Seconds());
           }
        }
        
        public void Setup()
        {
            if (_client != null)
                _client.Disconnect();

            _client = new IrcClient { ChannelSyncing = true, SendDelay = 200, AutoRetry = true };
            _client.OnChannelMessage += ircdata =>
                                            {
                                                if (ircdata.Nick == "Taimur")
                                                    Handle(ircdata.Message);
                                            };

            _client.OnInvite += (inviter, channel, ircdata) => _client.Join(ircdata.Message);
            
            _client.Connect("irc.rizon.net", 6667);
            _client.Login("[Yaar]", "[Yaar]");
            _client.Join("#yaar");

        }

        public override void Output(string output)
        {
            var lines = output.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                foreach (var channel in _client.JoinedChannels)
                    _client.Message(SendType.Message, channel, line);

                Brain.ListenerManager.Speech.Output(line);
            }
        }


    }
}