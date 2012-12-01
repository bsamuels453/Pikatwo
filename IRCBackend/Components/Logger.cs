using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PikaIRC;

namespace IRCBackend.Components {
    class Logger : IrcComponent {
        readonly IrcInstance.OnIrcInput _onIrcOutput;

        public Logger(IrcInstance.OnIrcInput onOutput){
            _onIrcOutput = onOutput;
            Enabled = true;
        }

        public void Dispose(){
            
        }

        public bool Enabled {
            get;
            set;
        }

        public void Reset(){

        }

        public void HandleMsg(IrcMsg msg, IrcInstance.SendIrcCmd sendMethod){
            //433 nick collision
            //376 motd end
            //"KICK"
            if (msg.Command == "433")
                _onIrcOutput.Invoke("Nick in use, attempting ghost");
            if (msg.Command == "376")
                _onIrcOutput.Invoke("Connection Successful");
            if (msg.Command == "353")
                _onIrcOutput.Invoke("Channel Joined");
            if (msg.Command == "KICK")
                _onIrcOutput.Invoke("Kicked from channel, attempting to rejoin");
        }
    }
}
