﻿#region

using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using PikaIRC;

#endregion

namespace IRCBackend.Components{
    internal class ConnectionTester : IrcComponent{
        #region Delegates

        public delegate void ReconnectMthd();

        #endregion

        readonly ReconnectMthd _reconnectMethod;
        readonly IrcInstance.SendIrcCmd _sendMethod;
        readonly Stopwatch _timeSincePongRecv;
        bool _pingThreadActive;
        Task _pingingThread;


        public ConnectionTester(ReconnectMthd reconnectMethod, IrcInstance.SendIrcCmd sendMethod){
            _reconnectMethod = reconnectMethod;
            _sendMethod = sendMethod;
            _pingThreadActive = false;
            _timeSincePongRecv = new Stopwatch();
        }

        #region IrcComponent Members

        public void Dispose(){
            _pingThreadActive = false;
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

        #endregion

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
                if (!_pingThreadActive){
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
                if (!_pingThreadActive){
                    Thread.Sleep(5000);
                    goto WaitForActive2;
                }
            }
        }
    }
}