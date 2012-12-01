#region

using System;
using IRCBackend;

#endregion

namespace PikaIRC{
// ReSharper disable InconsistentNaming
    public interface IrcComponent : IDisposable{
// ReSharper restore InconsistentNaming
        bool Enabled { get; set; }
        void Reset();
        void HandleMsg(IrcMsg msg, IrcInstance.SendIrcCmd sendMethod);
    }
}