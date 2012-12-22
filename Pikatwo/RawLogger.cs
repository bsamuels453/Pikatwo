using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using IrcClient;

namespace Pikatwo {
    class RawLogger : IrcComponent{
        readonly StreamWriter _sw;

        public RawLogger(){
            _sw = new StreamWriter("rawlog.txt");
        }

        public void Dispose(){
            
        }

        public void Reset(){
        }

        public void HandleMsg(IrcMsg msg, IrcInstance.SendIrcCmd sendMethod){
            _sw.WriteLine(msg.Prefix + " " + msg.Command + " " + msg.CommandParams + " " + msg.Trailing);
            _sw.Flush();
        }
    }
}
