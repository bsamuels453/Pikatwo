#region

using System;

#endregion

namespace IrcClient{
    //todo: trash IDisposable/Reset and replace with events

// ReSharper disable InconsistentNaming
    public interface IrcComponent : IDisposable{
// ReSharper restore InconsistentNaming
        void Reset();
        void HandleMsg(IrcMsg msg, IrcInstance.SendIrcCmd sendMethod);
    }
}