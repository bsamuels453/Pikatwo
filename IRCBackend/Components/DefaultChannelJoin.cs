using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PikaIRC.Components {
    internal class JoinDefaultChannel : IrcComponent {
        readonly string _channelToJoin;

        public JoinDefaultChannel(string channelName) {
            _channelToJoin = channelName;
            Enabled = true;
        }

        #region IrcComponent Members

        public void Dispose() {
        }

        public bool Enabled { get; set; }

        public void Reset() {
            Enabled = true;
        }

        public void HandleMsg(IrcMsg msg, IrcInstance.SendIrcCmd sendMethod) {
            if (msg.Command == "376") { //end of motd
                sendMethod.Invoke(
                    IrcCommand.Join,
                    _channelToJoin
                    );

                Enabled = false; //this component has no purpose after joining the default channel
            }
        }

        #endregion
    }
}
