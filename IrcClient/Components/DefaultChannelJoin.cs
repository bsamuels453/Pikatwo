﻿#region



#endregion

namespace IrcClient.Components{
    internal class JoinDefaultChannel : IrcComponent{
        readonly string _channelToJoin;

        public JoinDefaultChannel(string channelName){
            _channelToJoin = channelName;
        }

        #region IrcComponent Members

        public void Dispose(){
        }

        public void Reset(){
        }

        public void HandleMsg(IrcMsg msg, IrcInstance.SendIrcCmd sendMethod){
            if (msg.Command == "376"){ //end of motd
                sendMethod.Invoke(
                    IrcCommand.Join,
                    _channelToJoin,
                    doLog: true
                    );
            }
        }

        #endregion
    }
}