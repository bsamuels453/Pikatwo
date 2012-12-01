using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using IRCBackend;

namespace PikaIRC {
// ReSharper disable InconsistentNaming
    public interface IrcComponent : IDisposable {
// ReSharper restore InconsistentNaming
        bool Enabled { get; set; }
        void Reset();
        void HandleMsg(IrcMsg msg, IrcInstance.SendIrcCmd sendMethod);
    }
}
