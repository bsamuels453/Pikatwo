#region

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace PikaIRC{
    internal class JoinDefaultChannel : IrcComponent{
        readonly string _channelToJoin;

        public JoinDefaultChannel(string channelName){
            _channelToJoin = channelName;
            Enabled = true;
        }

        #region IrcComponent Members

        public void Dispose(){
        }

        public bool Enabled { get; set; }

        public void Reset(){
            Enabled = true;
        }

        public void HandleMsg(IrcMsg msg, IrcInstance.SendIrcCmd sendMethod){
            if (msg.Command == "376"){ //end of motd
                sendMethod.Invoke(
                    IrcCommand.Join,
                    _channelToJoin
                    );

                Enabled = false; //this component has no purpose after joining the default channel
            }
        }

        #endregion
    }

    internal class NickIdentifier : IrcComponent{
        readonly string _nick;
        readonly string _password;

        public NickIdentifier(string nick, string password){
            Enabled = true;
            _password = password;
            _nick = nick;
        }

        #region IrcComponent Members

        public void Dispose(){
        }

        public bool Enabled { get; set; }

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

        #endregion
    }

    internal class NickCollisionHandler : IrcComponent{
        readonly string _nick;
        readonly string _password;

        public NickCollisionHandler(string nick, string password){
            _nick = nick;
            _password = password;
            Enabled = true;
        }

        #region IrcComponent Members

        public void Dispose(){
        }

        public bool Enabled { get; set; }

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
                if (_password != null){
                    sendMethod.Invoke(
                        IrcCommand.Message,
                        "Nickserv",
                        string.Format("GHOST {0} {1}", _nick, _password)
                        );
                }
            }
            //test to see if ghost worked
            if (msg.Prefix.Contains("NickServ")
                && msg.CommandParams.Contains("has been ghosted.")){
                sendMethod.Invoke(
                    IrcCommand.ChangeNick,
                    _nick
                    );
            }
        }

        #endregion
    }

    internal class RejoinPostKick : IrcComponent{
        public RejoinPostKick(){
            Enabled = true;
        }

        #region IrcComponent Members

        public void Dispose(){
        }

        public bool Enabled { get; set; }

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

    internal class PingResponder : IrcComponent{
        public PingResponder(){
            Enabled = true;
        }

        #region IrcComponent Members

        public void Dispose(){
        }

        public bool Enabled { get; set; }

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

    internal class ConnectionTester : IrcComponent{
        public delegate void ReconnectMthd();

        bool _pingThreadActive;
        readonly ReconnectMthd _reconnectMethod;
        readonly IrcInstance.SendIrcCmd _sendMethod;
        Task _pingingThread;
        readonly Stopwatch _timeSincePongRecv;


        public ConnectionTester(ReconnectMthd reconnectMethod, IrcInstance.SendIrcCmd sendMethod){
            Enabled = true;
            _reconnectMethod = reconnectMethod;
            _sendMethod = sendMethod;
            _pingThreadActive = false;
            _timeSincePongRecv = new Stopwatch();
        }

        public void Dispose(){
            _pingThreadActive = false;
        }

        public bool Enabled {
            get;
            set;
        }

        public void Reset(){
            _pingThreadActive = false;
            lock (_timeSincePongRecv){
                _timeSincePongRecv.Reset();
            }
        }

        public void HandleMsg(IrcMsg msg, IrcInstance.SendIrcCmd sendMethod){
            if (msg.Command == "376"){
                _pingingThread = new Task(Pinger);
                _pingingThread.Start();
                _pingThreadActive = true;
            }

            if (msg.Command == "PONG"){
                lock (_timeSincePongRecv){
                    _timeSincePongRecv.Stop();
                }
            }
        }

        void Pinger(){
            while (true){
                _sendMethod.Invoke(
                    command: IrcCommand.Ping,
                    destination: "",
                    flushNow: true
                    );
                lock (_timeSincePongRecv){
                    _timeSincePongRecv.Restart();
                }

                Thread.Sleep(15000);

                //OH MAN HERES ANOTHER ONE
            WaitForActive1:
                if (!_pingThreadActive) {
                    Thread.Sleep(5000);
                    goto WaitForActive1;
                    break;
                }
                bool reconnect = false;
                lock (_timeSincePongRecv){
                    //if we dont recieve a pong after 15s, then disconnected
                    if (_timeSincePongRecv.IsRunning){
                        reconnect = true;
                    }
                }

                //avoid race condition with _timeSincePongRecv
                if (reconnect){
                    _reconnectMethod.Invoke();
                    break;
                }
                Thread.Sleep(60000);

            WaitForActive2:
                if (!_pingThreadActive) {
                    Thread.Sleep(5000);
                    goto WaitForActive2;
                }
            }
        }
    }
}