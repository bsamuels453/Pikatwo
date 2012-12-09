#region



#endregion

namespace IRCBackend.Components{
    internal class Logger : IrcComponent{
        readonly IrcInstance.OnIrcInput _onIrcOutput;

        public Logger(IrcInstance.OnIrcInput onOutput){
            _onIrcOutput = onOutput;
        }

        #region IrcComponent Members

        public void Dispose(){
        }

        public void Reset(){
        }

        public void HandleMsg(IrcMsg msg, IrcInstance.SendIrcCmd sendMethod){
            //433 nick collision
            //376 motd end
            //"KICK"
            if (msg.Command == "433")
                _onIrcOutput.Invoke("Nick in use, attempting ghost");
            if (msg.Command == "376")
                _onIrcOutput.Invoke("Connection Successful");
            if (msg.Command == "353")
                _onIrcOutput.Invoke("Channel Joined");
            if (msg.Command == "KICK")
                //_onIrcOutput.Invoke("Kicked from channel, attempting to rejoin");
            if (msg.Prefix.Contains("NickServ")
                && msg.Trailing.Contains("identified for"))
                _onIrcOutput.Invoke("Nickserv authentication successful");
            if (msg.Prefix.Contains("NickServ")
                && msg.Trailing.Contains("has been ghosted.")){
                _onIrcOutput.Invoke("Ghost command successful");
            }
        }
        #endregion
    }
}