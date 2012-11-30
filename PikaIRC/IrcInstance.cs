﻿#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace PikaIRC{
    public enum IrcCommand {
        Message,
        Join,
        NickChange
        //Disconnect
    }
    public partial class IrcInstance : IDisposable{
        public delegate void OnIrcInput(string msg);
        public delegate void SendIrcCmd(IrcCommand command, string destination, string param=null, bool flushNow=false);
        public event OnIrcInput OnIrcMsg;

        readonly string _serverAddress;
        readonly int _serverPort;
        readonly string _userNick;
        readonly string _userPass;
        readonly Task _readerThread;
        readonly string _defaultChannel;


        bool _disposed;

        public IrcInstance(IrcInitData initData) {
            _serverAddress = initData.Address;
            _serverPort = initData.Port;
            _userNick = initData.UserNick;
            _userPass = initData.UserPass;
            _defaultChannel = initData.DefaultChannel;

            _components = new List<IrcComponent>();
            _clientCommandQueue = new List<InternalTask>();

            _closeReaderThread = false;
            _disposed = false;

            _readerThread = new Task(ReaderThread);

            //setup builtin components
            _components.Add(new JoinDefaultChannel(_defaultChannel));
            _components.Add(new NickIdentifier(_userNick, _userPass));
            _components.Add(new NickCollisionHandler(_userNick, _userPass));
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
                case IrcCommand.NickChange:
                    cmd = "NICK";
                    break;
                default:
                    throw new Exception();
            }
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

        //this is the only case in which a synchronous method can call
        //one of the methods for use by the synchronouse read loop
        public void Connect(){
            if (_readerThread.Status != TaskStatus.Running) {
                _clientCommandQueue.Clear();
                foreach (var component in _components){
                    component.Reset();
                }

                InternalConnect();
                _readerThread.Start();
            }
        }

        public void Dispose(){
            if (!_disposed){
                lock (_clientCommandQueue) {
                    _clientCommandQueue.Add(DisposeThreadedAssets);
                }
                _disposed = true;
            }
        }
    }
    public struct IrcInitData {
        public string Address;
        public int Port;
        public string UserNick;
        public string UserPass;
        public string DefaultChannel;
    }
}