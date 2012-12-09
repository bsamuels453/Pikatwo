#region



#endregion

namespace IRCBackend.Components{
    internal class NickIdentifier : IrcComponent{
        readonly string _nick;
        readonly string _password;

        public NickIdentifier(string nick, string password){
            _password = password;
            _nick = nick;
        }

        #region IrcComponent Members

        public void Dispose(){
        }

        public void Reset(){
        }

        public void HandleMsg(IrcMsg msg, IrcInstance.SendIrcCmd sendMethod){
            if (msg.Command == "376"){ //end of motd
                sendMethod.Invoke(
                    IrcCommand.Message,
                    "Nickserv",
                    string.Format("IDENTIFY {0} {1}", _nick, _password)
                    );
            }
        }

        #endregion
    }
}