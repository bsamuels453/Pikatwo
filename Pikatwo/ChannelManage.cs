#region

using System;
using System.Collections.Generic;
using Meebey.SmartIrc4net;

#endregion

namespace Pikatwo{
    internal class ChannelManage : IrcComponent{
        readonly List<string> _channels;
        ClientInterface _ircInterface;

        public ChannelManage(string[] channels){
            _channels = new List<string>();
            _channels.AddRange(channels);
        }

        #region IrcComponent Members

        public ClientInterface IrcInterface{
            get { return _ircInterface; }
            set{
                _ircInterface = value;
                _ircInterface.Client.OnRegistered += OnRegistered;
                _ircInterface.Client.OnKick += OnKick;
            }
        }

        public void Update(long secsSinceStart){
        }

        public string[] GetCmdDocs(){
            return new string[0];
        }

        #endregion

        void OnRegistered(object sender, EventArgs eventArgs){
            foreach (var channel in _channels){
                _ircInterface.DebugLog("Joining channel " + channel);
                _ircInterface.Client.RfcJoin(channel);
            }
        }

        void OnKick(object sender, KickEventArgs kickEventArgs){
            if (kickEventArgs.Whom.Equals(_ircInterface.Nick)){
                _ircInterface.DebugLog("Kicked from channel " + kickEventArgs.Channel + ", attempting to rejoin");
                _ircInterface.Client.RfcJoin(kickEventArgs.Channel);
            }
        }
    }
}