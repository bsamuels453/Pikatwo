#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace IrcClient{
    public partial class IrcInstance{
        readonly List<IrcComponent> _components;
        readonly Stopwatch _timeSinceLastPing;
        bool _killReader;
        bool _exceptionOkay;

        string ReadStrm(){
            string input;
            try{
                input = _readStream.ReadLine();
            }
            catch (Exception){
                if (!_killReader && !_exceptionOkay){
                    _exceptionOkay = false;
                    _extLogWriter.Invoke("Exception in read stream");
                }
                input = null;
            }
            return input;
        }

        void ReaderThread(){
            while (true){
                Task<string> readerTsk = new Task<string>(ReadStrm);
                readerTsk.Start();

                string input = "";

                #region input reading loop

                while (true){
                    if (readerTsk.IsCompleted){
                        input = readerTsk.Result;
                        break;
                    }

                    lock (_timeSinceLastPing){
                        if (_timeSinceLastPing.ElapsedMilliseconds > 200000){ //200 sec
                            input = null;
                            _extLogWriter.Invoke("Ping timeout: " + _timeSinceLastPing.ElapsedMilliseconds/1000 + " seconds");
                            _exceptionOkay = true;
                            break;
                        }
                    }
                    if (_killReader)
                        break;

                    Thread.Sleep(100);
                }

                #endregion

                if (_killReader)
                    break;

                if (input == null){
                    _extLogWriter.Invoke("Disconnected from server");
                    Reconnect();
                    continue;
                }

                var msg = ParseInput(input);

                //ugly hack to get the hostname that we ended up getting connected to
                if (_hostName == ""){
                    _extLogWriter.Invoke("-Host name resolved");
                    _hostName = msg.Prefix;
                }

                foreach (var component in _components){
                    component.HandleMsg(msg, SendCmd);
                }

                if (msg.Command == "PING"){
                    lock (_timeSinceLastPing){
                        _timeSinceLastPing.Reset();
                        _timeSinceLastPing.Start();
                    }
                }
                _writeStream.Flush();


            }
        }

        IrcMsg ParseInput(string input){
            string prefix = "";
            string cmd;
            List<string> cmdParams = new List<string>();
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
            if (inputArgs.Count() > cmdIndex + 1){
                cmdIndex++;
                int trailingStartPos = -1;
                for (int i = cmdIndex; i < inputArgs.Count(); i++){
                    try{
                        if (inputArgs[i][0] == ':'){ //this denotes the beginning of trailing
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
                    catch (Exception e){
                        //this should never ever happen
                        StreamWriter sw = new StreamWriter("CRASH.txt");
                        sw.WriteLine("CRASH REPORT AT TIME INDEX " + DateTime.Now);
                        foreach (var arg in inputArgs){
                            sw.WriteLine("ARG: " + arg);
                        }
                        sw.WriteLine("INPUT: " + input);
                        sw.WriteLine("I: " + i);
                        sw.Flush();
                        sw.Close();
                    }
                }
                if (trailingStartPos > cmdIndex){ //now grab everything between the command and the trailing
                    for (int i = cmdIndex; i < trailingStartPos; i++){
                        //if (!inputArgs[i].Contains(_userNick)){
                        cmdParams.Add(inputArgs[i] + " ");
                        //}
                    }
                    //clip trailing whitespace
                    if (cmdParams.Any()){
                        if (cmdParams.Last().Last() == ' ')
                            cmdParams[cmdParams.Count - 1] =
                                cmdParams.Last().Remove(
                                    cmdParams.Last().Count() - 1
                                    );
                    }
                }
            }

            //this saves us from loads of error checking later
            while (cmdParams.Count < 5)
                cmdParams.Add("");

            var retMsg = new IrcMsg();
            retMsg.Prefix = prefix;
            retMsg.Command = cmd;
            retMsg.CommandParams = cmdParams.ToArray();
            retMsg.Trailing = trailing;
            return retMsg;
        }

        void DisposeThreadedAssets(){
            _client.Close();
            _readStream.Close();
            _writeStream.Close();
            foreach (var component in _components){
                component.Dispose();
            }
            _hostName = "";
        }

        void Reconnect(){
            _extLogWriter.Invoke("Reconnecting to server");
            DisposeThreadedAssets();
            foreach (var component in _components){
                component.Reset();
            }
            InvokeConnect();
        }

        //while this method isnt explicitly threaded, it's only invoked  by the reader thread or an 
        //external thread only when the reader thread is dead, so there's no chance for collisions
        void InvokeConnect(){
            if (_client != null){
                _readStream.Close();
                _writeStream.Close();
                _client.Close();
            }
            //I WONT TELL ANYONE IF YOU WONT
            RetryConnect:
            //AHAHAHAHAHAHAAHAH
            try{
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
            lock (_timeSinceLastPing) {
                _timeSinceLastPing.Reset();
                _timeSinceLastPing.Start();
            }
            _exceptionOkay = false;
        }

        #region stuff that synchronous methods shouldnt touch except for ctor
        TcpClient _client;
        StreamReader _readStream;
        StreamWriter _writeStream;
        #endregion
    }
}