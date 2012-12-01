﻿#region

using System;
using PikaIRC;

#endregion

namespace IRCBackend{
// ReSharper disable InconsistentNaming
    public interface IrcComponent : IDisposable{
// ReSharper restore InconsistentNaming
        void Reset();
        void HandleMsg(IrcMsg msg, IrcInstance.SendIrcCmd sendMethod);
    }
}