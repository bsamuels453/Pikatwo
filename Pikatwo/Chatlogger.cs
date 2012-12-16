using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using IrcClient;

namespace Pikatwo {
    internal class Chatlogger : IrcComponent{
        StreamWriter _writer;

        public Chatlogger(){
            _writer = new StreamWriter("data/logs.txt", true);
        }

        public void Dispose(){
            _writer.Close();
            _writer.Dispose();
            _writer = null;
        }

        public void Reset(){
            if (_writer == null){
                _writer = new StreamWriter("data/logs.txt", true);
            }
        }

        public void HandleMsg(IrcMsg msg, IrcInstance.SendIrcCmd sendMethod){
            if(msg.CommandParams.Any()){
                if (msg.Command == "PRIVMSG" && msg.CommandParams[0].Contains("#")){//thisll narrow input down to channel messages
                    _writer.WriteLine(
                        string.Format("{0} {1} {2}",
                                      (int) (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds,
                                      msg.Prefix,
                                      msg.Trailing
                            )
                        );
                    _writer.Flush();
                }
            }
        }
    }
}
