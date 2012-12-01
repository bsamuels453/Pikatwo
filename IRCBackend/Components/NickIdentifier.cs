#region

using IRCBackend;

#endregion

namespace PikaIRC.Components{
    internal class NickIdentifier : IrcComponent{
        readonly string _nick;
        readonly string _password;

        public NickIdentifier(string nick, string password){
            Enabled = true;
            _password = password;
            _nick = nick;
        }

        #region IrcComponent Members

        public void Dispose(){
        }

        public bool Enabled { get; set; }

        public void Reset(){
            Enabled = true;
        }

        public void HandleMsg(IrcMsg msg, IrcInstance.SendIrcCmd sendMethod){
            if (msg.Command == "376"){ //end of motd
                sendMethod.Invoke(
                    IrcCommand.Message,
                    "Nickserv",
                    string.Format("IDENTIFY {0} {1}", _nick, _password)
                    );

                Enabled = false; //this component has no purpose after joining the default channel
            }
        }

        #endregion
    }
}