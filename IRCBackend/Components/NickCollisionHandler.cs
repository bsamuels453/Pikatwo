#region

using System;

#endregion

namespace IRCBackend.Components{
    internal class NickCollisionHandler : IrcComponent{
        readonly string _nick;
        readonly string _password;

        public NickCollisionHandler(string nick, string password){
            _nick = nick;
            _password = password;
        }

        #region IrcComponent Members

        public void Dispose(){
        }

        public void Reset(){
        }

        public void HandleMsg(IrcMsg msg, IrcInstance.SendIrcCmd sendMethod){
            //someone else is already using this nick
            if (msg.Command == "433"){
                //change the nick
                sendMethod.Invoke(
                    IrcCommand.ChangeNick,
                    _nick + DateTime.Now.Millisecond
                    );

                //ghost them if possible
                if (!string.IsNullOrEmpty(_password)){
                    sendMethod.Invoke(
                        IrcCommand.Message,
                        "Nickserv",
                        string.Format("GHOST {0} {1}", _nick, _password)
                        );
                }
            }
            //test to see if ghost worked
            if (msg.Prefix.Contains("NickServ")
                && msg.Trailing.Contains("has been ghosted.")){
                sendMethod.Invoke(
                    IrcCommand.ChangeNick,
                    _nick
                    );
            }
        }

        #endregion
    }
}