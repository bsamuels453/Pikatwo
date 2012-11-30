using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PikaIRC {
// ReSharper disable InconsistentNaming
    public interface IrcComponent : IDisposable {
// ReSharper restore InconsistentNaming
        bool Enabled { get; set; }
        void HandleMsg(IrcMsg msg, StreamWriter writer);
    }
}
