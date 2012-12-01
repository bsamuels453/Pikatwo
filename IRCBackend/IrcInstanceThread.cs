using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using PikaIRC;

namespace IRCBackend {
    public partial class IrcInstance {
        delegate void InternalTask();

        #region stuff that synchronous methods shouldnt touch except for ctor
        bool _closeReaderThread;

        TcpClient _client;
        StreamReader _readStream;
        StreamWriter _writeStream;

        #endregion

        //these must be locked before using with exception of ctor
        readonly List<IrcComponent> _components;
        readonly List<InternalTask> _clientCommandQueue;

        void ReaderThread(){
            string input = "";
            while (true){
                try {
                    input = _readStream.ReadLine();
                }
                catch (IOException e){
                    Reconnect();
                }

                lock (_clientCommandQueue){
                    for(int i=0; i<_clientCommandQueue.Count; i++){
                        _clientCommandQueue[i].Invoke();
                    }
                }

                if (_closeReaderThread)
                    break;

                var msg = ParseInput(input);

                                //ugly hack to get the hostname that we ended up getting connected to
                lock (_hostName){
                    if (_hostName == ""){
                        _onIrcOutput.Invoke("Host name resolved");
                        _hostName = msg.Prefix;
                    }
                }

                foreach (var component in _components){
                    if (component.Enabled){
                        component.HandleMsg(msg, SendCmd);
                    }
                }
                _writeStream.Flush();
            }
        }

        IrcMsg ParseInput(string input){
            string prefix = "";
            string cmd = "";
            string cmdParams = "";
            string trailing = "";
            int cmdIndex;

            List<string> inputArgs = input.Split(' ').ToList();

            //generate prefix/cmd
            if (inputArgs[0][0] == ':'){
                prefix = inputArgs[0];
                cmd = inputArgs[1];
                cmdIndex = 1;
            }
            else{
                cmd = inputArgs[0];
                cmdIndex = 0;
            }

            //genreate commandparams and trailing
            if (inputArgs.Count() > cmdIndex+1) {
                cmdIndex++;
                int trailingStartPos = -1;
                for (int i = cmdIndex; i < inputArgs.Count(); i++) {
                    if (inputArgs[i][0] == ':'){
                        trailing = "";

                        //concat the args into a string
                        var strLi = inputArgs.GetRange(i, inputArgs.Count - i);
                        foreach (var s in strLi){
                            trailing += s + " ";
                        }
                        //remove trailing whitespace
                        trailing = trailing.Remove(trailing.Count() - 1);
                        trailingStartPos = i;
                        break;
                    }
                }
                if (trailingStartPos > cmdIndex) {
                    for (int i = cmdIndex; i < trailingStartPos; i++){
                        if (!inputArgs[i].Contains(_userNick)){
                            cmdParams += inputArgs[i] + " ";
                        }
                    }
                    //clip trailing whitespace
                    if (cmdParams.Any()){
                        if (cmdParams.Last() == ' ')
                            cmdParams = cmdParams.Remove(cmdParams.Count() - 1);
                    }
                }
            }

            var retMsg = new IrcMsg();
            retMsg.Prefix = prefix;
            retMsg.Command = cmd;
            retMsg.CommandParams = cmdParams;
            retMsg.Trailing = trailing;


            return retMsg;
        }

        void DisposeThreadedAssets(){
            _client.Close();
            _readStream.Close();
            _writeStream.Close();
            _closeReaderThread = true;
            foreach (var component in _components){
                component.Dispose();
            }
            lock (_hostName){
                _hostName = "";
            }
        }

        void Reconnect(){
            lock (_clientCommandQueue){
                _clientCommandQueue.Add(InternalReconnect);
            }
        }

        void InternalReconnect(){
            DisposeThreadedAssets();
            _clientCommandQueue.Clear();
            foreach (var component in _components) {
                component.Reset();
            }
            InternalConnect();
            if (_readerThread.Status != TaskStatus.Running){
                _readerThread.Start();
            }
            else{
                _closeReaderThread = false;
            }
        }

        void InternalConnect() {
            if (_client != null) {
                _readStream.Close();
                _writeStream.Close();
                _client.Close();
            }
            //I WONT TELL ANYONE IF YOU WONT
            RetryConnect:
            //AHAHAHAHAHAHAAHAH
            try {
                _client = new TcpClient(_serverAddress, _serverPort);
            }
            catch (SocketException){
                Thread.Sleep(5000);
                goto RetryConnect;
            }
            _client.ReceiveBufferSize = 65536;

            var stream = _client.GetStream();
            _readStream = new StreamReader(stream);
            _writeStream = new StreamWriter(stream);

            _writeStream.WriteLine(
                string.Format("NICK {0}\r\n", _userNick)
                );
            _writeStream.Flush();

            _writeStream.WriteLine(
                string.Format("USER {0} {1} * :{2}\r\n", "pikacs", _serverAddress, "pikacs")
                );
            _writeStream.Flush();

        }

    }
}
