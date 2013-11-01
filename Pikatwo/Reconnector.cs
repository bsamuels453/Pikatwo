#region

using System;

#endregion

namespace Pikatwo{
    internal class Reconnector : IrcComponent{
        ClientInterface _ircClient;

        #region IrcComponent Members

        public ClientInterface IrcClient{
            get { return _ircClient; }
            set{
                _ircClient = value;
                _ircClient.OnDisconnect += OnDisconnect;
            }
        }

        public void Update(long secsSinceStart){
        }

        #endregion

        void OnDisconnect(object sender, EventArgs eventArgs){
            _ircClient.Connect();
        }
    }
}