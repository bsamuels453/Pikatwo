#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace PikaIRC{
    public partial class IrcInstance : IDisposable{
        readonly string _serverAddress;
        readonly int _serverPort;
        readonly string _userNick;
        readonly string _userPass;
        readonly Task _readerThread;


        bool _disposed;
        bool _isConnected;

        public IrcInstance(IrcInitData initData) {
            _serverAddress = initData.Address;
            _serverPort = initData.Port;
            _userNick = initData.UserNick;
            _userPass = initData.UserPass;

            _components = new List<IrcComponent>();
            _clientCommandQueue = new List<InternalTask>();

            _closeReaderThread = false;
            _isConnected = false;
            _disposed = false;

            _readerThread = new Task(ReaderThread);
        }

        //this is the only case in which a synchronous method can call
        //one of the methods for use by the synchronouse read loop
        public void Connect(){
            if (!_isConnected){
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