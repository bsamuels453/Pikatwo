using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PikaIRC.Components {
    internal class NickCollisionHandler : IrcComponent {
        readonly string _nick;
        readonly string _password;

        public NickCollisionHandler(string nick, string password) {
            _nick = nick;
            _password = password;
            Enabled = true;
        }

        #region IrcComponent Members

        public void Dispose() {
        }

        public bool Enabled { get; set; }

        public void Reset() {
        }

        public void HandleMsg(IrcMsg msg, IrcInstance.SendIrcCmd sendMethod) {
            //someone else is already using this nick
            if (msg.Command == "433") {
                //change the nick
                sendMethod.Invoke(
                    IrcCommand.ChangeNick,
                    _nick + DateTime.Now.Millisecond
                    );

                //ghost them if possible
                if (_password != null) {
                    sendMethod.Invoke(
                        IrcCommand.Message,
                        "Nickserv",
                        string.Format("GHOST {0} {1}", _nick, _password)
                        );
                }
            }
            //test to see if ghost worked
            if (msg.Prefix.Contains("NickServ")
                && msg.CommandParams.Contains("has been ghosted.")) {
                sendMethod.Invoke(
                    IrcCommand.ChangeNick,
                    _nick
                    );
            }
        }

        #endregion
    }
}
