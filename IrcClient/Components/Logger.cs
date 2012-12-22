#region



#endregion

namespace IrcClient.Components{
    internal class Logger : IrcComponent{
        readonly IrcInstance.OnIrcInput _onIrcOutput;
        readonly string _userNick;
        
        public Logger(IrcInstance.OnIrcInput onOutput, string userNick){
            _onIrcOutput = onOutput;
            _userNick = userNick;
        }

        #region IrcComponent Members

        public void Dispose(){
        }

        public void Reset(){
        }

        public void HandleMsg(IrcMsg msg, IrcInstance.SendIrcCmd sendMethod){
            if (msg.Command == "433")
                _onIrcOutput.Invoke("-Nick in use, attempting ghost if password provided");
            if (msg.Command == "376")
                _onIrcOutput.Invoke("-Connection Successful");
            if (msg.Command == "366")
                _onIrcOutput.Invoke("-Channel Joined");
            if (msg.Command == "KICK" && msg.CommandParams[1] == _userNick)
                _onIrcOutput.Invoke("-Kicked from channel, attempting to rejoin");
            if (msg.Prefix.Contains("NickServ")
                && msg.Trailing.Contains("identified for"))
                _onIrcOutput.Invoke("-Nickserv authentication successful");
            if (msg.Prefix.Contains("NickServ")
                && msg.Trailing.Contains("has been ghosted.")){
                _onIrcOutput.Invoke("-Ghost command successful");
            }
            if (msg.Command == "NOTICE")
                _onIrcOutput.Invoke("<NOTICE " + msg.Trailing);
            if (msg.Command == "PONG")
                _onIrcOutput.Invoke("<PONG");
        }
        #endregion
    }
}