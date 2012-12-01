#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using IRCBackend.Components;
using PikaIRC;

#endregion

namespace IRCBackend{
    public enum IrcCommand{
        Message,
        Join,
        ChangeNick,
        Ping,
        Pong,
        Quit
        //Disconnect
    }

    public partial class IrcInstance : IDisposable{
        #region Delegates

        public delegate void OnIrcInput(string msg);

        public delegate void SendIrcCmd(IrcCommand command, string destination, string param = null, bool flushNow = false);

        #endregion

        readonly string _defaultChannel;

        readonly OnIrcInput _onIrcOutput;
        readonly Task _readerThread;

        readonly string _serverAddress;
        readonly int _serverPort;
        readonly string _userNick;
        readonly string _userPass;


        bool _disposed;
        string _hostName;

        public IrcInstance(IrcInitData initData, OnIrcInput loggingCallback, IEnumerable<IrcComponent> components = null){
            _serverAddress = initData.Address;
            _serverPort = initData.Port;
            _userNick = initData.UserNick;
            _defaultChannel = initData.DefaultChannel;
            _userPass = initData.UserPass ?? "";

            _components = new List<IrcComponent>();
            _clientCommandQueue = new List<InternalTask>();

            _closeReaderThread = false;
            _disposed = false;
            _hostName = "";

            _readerThread = new Task(ReaderThread);

            //setup builtin components
            _components.Add(new JoinDefaultChannel(_defaultChannel));
            if (_userPass != ""){
                _components.Add(new NickIdentifier(_userNick, _userPass));
            }
            _components.Add(new NickCollisionHandler(_userNick, _userPass));
            _components.Add(new PingResponder());
            _components.Add(new RejoinPostKick());
            _components.Add(new ConnectionTester(Reconnect, SendCmd));
            if (loggingCallback != null){
                _onIrcOutput = loggingCallback;
                _components.Add(new Logger(_onIrcOutput));
            }
            if (components != null){
                _components.AddRange(components);
            }
        }

        #region IDisposable Members

        public void Dispose(){
            if (!_disposed){
                SendCmd(IrcCommand.Quit, "", null, true);
                lock (_clientCommandQueue){
                    _clientCommandQueue.Add(DisposeThreadedAssets);
                }
                _disposed = true;
            }
        }

        #endregion

        //this is the only case in which a synchronous method can call
        //one of the methods for use by the synchronouse read loop
        public void Connect(){
            _onIrcOutput.Invoke("Starting connection");
            if (_readerThread.Status != TaskStatus.Running){
                _clientCommandQueue.Clear();
                foreach (var component in _components){
                    component.Reset();
                }

                InternalConnect();
                _readerThread.Start();
            }
        }

        public void SendCmd(IrcCommand command, string destination, string param = null, bool flushNow = false){
            Debug.Assert(_writeStream != null);
            string cmd;
            switch (command){
                case IrcCommand.Message:
                    cmd = "PRIVMSG";
                    break;
                case IrcCommand.Join:
                    cmd = "JOIN";
                    break;
                case IrcCommand.ChangeNick:
                    cmd = "NICK";
                    break;
                case IrcCommand.Pong:
                    cmd = "PONG";
                    break;
                case IrcCommand.Ping:
                    cmd = "PING";
                    destination = _hostName;
                    break;
                case IrcCommand.Quit:
                    cmd = "QUIT";
                    break;
                default:
                    throw new Exception();
            }
            lock (_writeStream){
                if (param != null)
                    _writeStream.WriteLine(
                        string.Format("{0} {1} :{2}\r\n", cmd, destination, param)
                        );

                else
                    _writeStream.WriteLine(
                        string.Format("{0} {1}\r\n", cmd, destination)
                        );

                if (flushNow){
                    _writeStream.Flush();
                }
            }
        }
    }

    public struct IrcInitData{
        public string Address;
        public string DefaultChannel;
        public int Port;
        public string UserNick;
        public string UserPass;
    }
}