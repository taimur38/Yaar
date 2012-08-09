using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IrcDotNet;

namespace Yaar.Listeners
{
    public class IRCListener : ListenerBase
    {
        private IrcClient _client;

        public IRCListener(Pipe pipe)
            : base(pipe)
        {
            _client = new IrcClient();
            _client.FloodPreventer = new IrcStandardFloodPreventer(4, 2000);
        }

        public override void Loop()
        {
           while(true)
           {
               using (var mre = new ManualResetEvent(false))
               {
                   if(_client != null)
                       _client.Disconnect();

                   _client = new IrcClient();
                   _client.Registered += (sender, args) =>
                                        {
                                            _client.LocalUser.JoinedChannel += (sender1, args1) =>
                                            {
                                                args1.Channel.MessageReceived += (o, eventArgs) =>
                                                {
                                                    if (eventArgs.Source.Name == "Taimur")
                                                        Handle(eventArgs.Text);
                                                };
                                                
                                                Output("Joined " + args1.Channel.Name);
                                                mre.Set();
                                            };
                                            
                                            _client.Channels.Join("#yaar");
                                        };

                   _client.Connect("irc.rizon.net", 6667, false, new IrcUserRegistrationInfo()
                                                                     {
                                                                         NickName = "[Yaar]",
                                                                         UserName = "[Yaar]",
                                                                         RealName = "[Yaar]"
                                                                     });
                   mre.WaitOne(30.Seconds());
                   while(_client.IsConnected)
                       5.Seconds().Sleep();
               }
               Brain.ListenerManager.CurrentListener.Output("Failed to connect to Rizon.");
           }
        }

        public override void Output(string output)
        {
            if (_client.IsRegistered)
            {
                var lines = output.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    foreach (var channel in _client.Channels)
                    {
                        _client.LocalUser.SendMessage(channel, line);
                    }
                }
            }

            if(!Brain.Muted)
                Brain.ListenerManager.Speech.Output(output);
        }


    }
}