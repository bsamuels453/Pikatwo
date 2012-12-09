#region



#endregion

namespace IRCBackend.Components{
    internal class PingResponder : IrcComponent{
        #region IrcComponent Members

        public void Dispose(){
        }

        public void Reset(){
        }

        public void HandleMsg(IrcMsg msg, IrcInstance.SendIrcCmd sendMethod){
            if (msg.Command == "PING"){ //laugh it up kuraitou
                sendMethod.Invoke(
                    IrcCommand.Pong,
                    msg.Trailing
                    );
            }
        }

        #endregion
    }
}