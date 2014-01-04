#region

using System;
using System.Threading;

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
                _ircInterface.OnIrcCommand += IrcInterfaceOnOnIrcCommand;
            }
        }

        public void Update(long secsSinceStart){
        }

        public string[] GetCmdDocs(){
            return new string[0];
        }

        #endregion

        void IrcInterfaceOnOnIrcCommand(OnCommandArgs args){
            if (args.AuthLevel == AuthLevel.Admin){
                if (args.Message == ".disconnect"){
                    _ircInterface.Client.RfcQuit("Manual disconnect request recv.d");
                    Thread.Sleep(300);
                    _ircInterface.DebugLog("Manually disconnected from the server.");
                    _ircInterface.KillClient = true;
                }
                if (args.Message == ".redeploy"){
                    _ircInterface.Client.RfcQuit("Deploying to new production build.");
                    Thread.Sleep(300);
                    _ircInterface.DebugLog("Deploying to new production build, manually disconnected from the server.");
                    _ircInterface.KillClient = true;
                }
            }
        }

        void OnDisconnect(object sender, EventArgs eventArgs){
            _ircInterface.DebugLog("Disconnected from server. Attempting reconnect...");
            _ircInterface.Connect();
            _ircInterface.DebugLog("Reconnection successful.");
        }
    }
}