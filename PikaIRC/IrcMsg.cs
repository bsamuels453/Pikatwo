using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PikaIRC {
    public class IrcMsg {
        public string Prefix;
        public string Command;
        public string CommandParams;
        public string Destination;
    }
}
