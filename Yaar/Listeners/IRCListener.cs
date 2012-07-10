using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            _client = new IrcClient {ChannelSyncing = true, SendDelay = 200, AutoRetry = true};
            _client.OnChannelMessage += _client_OnChannelMessage;
            _client.OnInvite += _client_OnInvite;
            _client.Connect("irc.freenode.net", 6667);
            _client.Login("[Yaar]", "[Yaar]");
            _client.Join("#yaar");
            _client.Listen();
            Output("Yaar online.");
        }

        private void _client_OnInvite(string inviter, string channel, Data ircdata)
        {
            _client.Join(ircdata.Message);
        }

        private void _client_OnChannelMessage(Data ircdata)
        {
            if(ircdata.Nick == "AbstractClass")
                Handle(ircdata.Message);
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