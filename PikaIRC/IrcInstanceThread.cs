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

        #region stuff that synchronous methods shouldnt touch except for ctor
        bool _closeReaderThread;

        TcpClient _client;
        StreamReader _clientRead;
        StreamWriter _clientWrite;

        #endregion

        //these must be locked before using with exception of ctor
        readonly List<IrcComponent> _components;
        readonly List<InternalTask> _clientCommandQueue;

        void ReaderThread(){
            string input;
            while ((input = _clientRead.ReadLine()) != null){
                lock (_clientCommandQueue){
                    foreach (var task in _clientCommandQueue){
                        task.Invoke();
                    }
                }

                if (_closeReaderThread){
                    break;
                }

                ParseInput(input);

            }
        }

        IrcMsg ParseInput(string input){
            string prefix = null;
            string cmd = null;
            string cmdParams = null;
            string destination = null;

            List<string> inputArgs = input.Split(' ').ToList();

            //generate prefix/cmd/dest
            //for some reason, "PING" is the prefix during pings instead of the 
            //command, so fix for that
            if (inputArgs[0] != "PING") {
                prefix = inputArgs[0];
                cmd = inputArgs[1];
                destination = inputArgs[2];
            }
            else{
                cmd = inputArgs[0];
                cmdParams = inputArgs[1];
            }

            //generate command parameters
            for (int i = 3; i < inputArgs.Count(); i++) {
                if (inputArgs[i][0] == ':'){
                    cmdParams = "";

                    //concat the args into a string
                    var strLi = inputArgs.GetRange(i, inputArgs.Count - i);
                    foreach (var s in strLi){
                        cmdParams += s + " ";
                    }
                    //remove trailing whitespace
                    cmdParams = cmdParams.Remove(cmdParams.Count() - 1);
                }
            }

            var retMsg = new IrcMsg();
            retMsg.Prefix = prefix;
            retMsg.Command = cmd;
            retMsg.CommandParams = cmdParams;
            retMsg.Destination = destination;
        }

        void DisposeThreadedAssets(){
            _client.Close();
            _clientRead.Close();
            _clientWrite.Close();
            _isConnected = false;
            _closeReaderThread = true;
            foreach (var component in _components){
                component.Dispose();
            }
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
