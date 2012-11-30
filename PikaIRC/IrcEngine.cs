using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PikaIRC {
    public partial class IrcInstance {
        delegate void InternalTask();

        #region stuff that synchronous methods shouldnt touch except for constructor
        bool _closeReaderThread;
        bool _isConnected;

        TcpClient _client;
        StreamReader _clientRead;
        StreamWriter _clientWrite;
        Task _commandLoopTask;
        #endregion

        readonly List<IrcComponent> _components;
        event InternalTask ClientCommandQueue;


        void ReaderThread() {
            string input;
            while ((input = _clientRead.ReadLine()) != null) {
                if (ClientCommandQueue != null)
                    ClientCommandQueue.Invoke();
                ClientCommandQueue = null;

                if (_closeReaderThread){
                    break;
                }
 
            }
        }

        void DisposeStreams(){
            _client.Close();
            _clientRead.Close();
            _clientWrite.Close();
            _isConnected = false;
            _closeReaderThread = true;
        }

        void InternalConnect() {
            if (_isConnected) {
                return; //should this go quietly?
            }

            if (_client != null) {
                _clientRead.Close();
                _clientWrite.Close();
                _client.Close();
            }

            _client = new TcpClient(_serverAddress, _serverPort);
            _client.ReceiveBufferSize = 65536;

            var stream = _client.GetStream();
            _clientRead = new StreamReader(stream);
            _clientWrite = new StreamWriter(stream);

            _clientWrite.WriteLine(
                string.Format("NICK {0}\r\n", _userNick)
                );
            _clientWrite.Flush();

            _clientWrite.WriteLine(
                string.Format("USER {0} {1} * :{2}\r\n", "pikacs", _serverAddress, "pikacs")
                );
            _clientWrite.Flush();

            _isConnected = true;
        }
    }
}
