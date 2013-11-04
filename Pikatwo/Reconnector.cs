#region

using System;

#endregion

namespace Pikatwo{
    internal class Reconnector : IrcComponent{
        ClientInterface _ircInterface;

        #region IrcComponent Members

        public ClientInterface IrcInterface{
            get { return _ircInterface; }
            set{
                _ircInterface = value;
                _ircInterface.Client.OnDisconnected += OnDisconnect;
            }
        }

        public void Update(long secsSinceStart){
        }

        #endregion

        void OnDisconnect(object sender, EventArgs eventArgs){
            _ircInterface.DebugLog("Disconnected from server. Attempting reconnect...");
            _ircInterface.Connect();
            _ircInterface.DebugLog("Reconnection successful.");
        }
    }
}