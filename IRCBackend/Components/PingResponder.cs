using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRCBackend;

namespace PikaIRC.Components {
    internal class PingResponder : IrcComponent {
        public PingResponder() {
            Enabled = true;
        }

        #region IrcComponent Members

        public void Dispose() {
        }

        public bool Enabled { get; set; }

        public void Reset() {
        }

        public void HandleMsg(IrcMsg msg, IrcInstance.SendIrcCmd sendMethod) {
            if (msg.Command == "PING") { //laugh it up kuraitou
                sendMethod.Invoke(
                    IrcCommand.Pong,
                    msg.Trailing
                    );
            }
        }

        #endregion
    }
}
