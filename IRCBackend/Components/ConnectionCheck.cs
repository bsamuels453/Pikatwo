using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IRCBackend;

namespace PikaIRC.Components {
    internal class ConnectionTester : IrcComponent {
        public delegate void ReconnectMthd();

        bool _pingThreadActive;
        readonly ReconnectMthd _reconnectMethod;
        readonly IrcInstance.SendIrcCmd _sendMethod;
        Task _pingingThread;
        readonly Stopwatch _timeSincePongRecv;


        public ConnectionTester(ReconnectMthd reconnectMethod, IrcInstance.SendIrcCmd sendMethod) {
            Enabled = true;
            _reconnectMethod = reconnectMethod;
            _sendMethod = sendMethod;
            _pingThreadActive = false;
            _timeSincePongRecv = new Stopwatch();
        }

        public void Dispose() {
            _pingThreadActive = false;
        }

        public bool Enabled {
            get;
            set;
        }

        public void Reset() {
            _pingThreadActive = false;
            lock (_timeSincePongRecv) {
                _timeSincePongRecv.Reset();
            }
        }

        public void HandleMsg(IrcMsg msg, IrcInstance.SendIrcCmd sendMethod) {
            if (msg.Command == "376") {
                _pingingThread = new Task(Pinger);
                _pingingThread.Start();
                _pingThreadActive = true;
            }

            if (msg.Command == "PONG") {
                lock (_timeSincePongRecv) {
                    _timeSincePongRecv.Stop();
                }
            }
        }

        void Pinger() {
            while (true) {
                _sendMethod.Invoke(
                    command: IrcCommand.Ping,
                    destination: "",
                    flushNow: true
                    );
                lock (_timeSincePongRecv) {
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
                lock (_timeSincePongRecv) {
                    //if we dont recieve a pong after 15s, then disconnected
                    if (_timeSincePongRecv.IsRunning) {
                        reconnect = true;
                    }
                }

                //avoid race condition with _timeSincePongRecv
                if (reconnect) {
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
