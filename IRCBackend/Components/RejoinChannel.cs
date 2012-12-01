#region

using PikaIRC;

#endregion

namespace IRCBackend.Components{
    internal class RejoinPostKick : IrcComponent{
        #region IrcComponent Members

        public void Dispose(){
        }

        public void Reset(){
        }

        public void HandleMsg(IrcMsg msg, IrcInstance.SendIrcCmd sendMethod){
            if (msg.Command == "KICK"){
                sendMethod.Invoke(
                    IrcCommand.Join,
                    msg.CommandParams
                    );
            }
        }

        #endregion
    }
}