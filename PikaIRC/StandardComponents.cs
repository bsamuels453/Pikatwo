#region

using System;
using System.IO;

#endregion

namespace PikaIRC{
    internal class JoinDefaultChannel : IrcComponent{
        readonly string _channelToJoin;

        public JoinDefaultChannel(string channelName){
            _channelToJoin = channelName;
            Enabled = true;
        }

        #region IrcComponent Members

        public void Dispose(){}

        public bool Enabled{get;set;}

        public void Reset(){
            Enabled = true;
        }

        public void HandleMsg(IrcMsg msg, IrcInstance.SendIrcCmd sendMethod) {
            if (msg.Command == "376"){ //end of motd
                sendMethod.Invoke(
                    IrcCommand.Join,
                    _channelToJoin
                    );
               
                Enabled = false;//this component has no purpose after joining the default channel
            }
        }

        #endregion
    }

    internal class NickIdentifier : IrcComponent{
        readonly string _password;
        readonly string _nick;

        public NickIdentifier(string nick, string password){
            Enabled = true;
            _password = password;
            _nick = nick;
        }

        public void Dispose(){
        }

        public bool Enabled {get;set;}

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
    }

    internal class NickCollisionHandler : IrcComponent{
        readonly string _nick;
        readonly string _password;

        public NickCollisionHandler(string nick, string password){
            _nick = nick;
            _password = password;
            Enabled = true;
        }


        public void Dispose(){

        }

        public bool Enabled {
            get;
            set;
        }

        public void Reset(){
        }

        public void HandleMsg(IrcMsg msg, IrcInstance.SendIrcCmd sendMethod) {
            //someone else is already using this nick
            if (msg.Command == "433"){
                //change the nick
                sendMethod.Invoke(
                    IrcCommand.ChangeNick,
                    _nick + DateTime.Now.Millisecond
                    );

                //ghost them if possible
                if (_password != null){
                    sendMethod.Invoke(
                        IrcCommand.Message,
                        "Nickserv",
                        string.Format("GHOST {0} {1}", _nick, _password)
                        );
                }
            }
            //test to see if ghost worked
            if (msg.Prefix != null && msg.CommandParams != null){
                if (msg.Prefix.Contains("NickServ")
                    && msg.CommandParams.Contains("has been ghosted.")){
                    sendMethod.Invoke(
                        IrcCommand.ChangeNick,
                        _nick
                        );
                }
            }
        }
    }

    internal class PingResponder : IrcComponent{
        public PingResponder(){
            Enabled = true;
        }

        public void Dispose(){
        }

        public bool Enabled {
            get;
            set;
        }

        public void Reset(){
        }

        public void HandleMsg(IrcMsg msg, IrcInstance.SendIrcCmd sendMethod){
            if (msg.Command == "PING"){//laugh it up kuraitou
                sendMethod.Invoke(
                    IrcCommand.Pong,
                    msg.CommandParams
                    );
            }
        }
    }
}