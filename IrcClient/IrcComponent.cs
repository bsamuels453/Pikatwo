#region

using System;

#endregion

namespace IrcClient{
// ReSharper disable InconsistentNaming
    public interface IrcComponent : IDisposable{
// ReSharper restore InconsistentNaming
        void Reset();
        void HandleMsg(IrcMsg msg, IrcInstance.SendIrcCmd sendMethod);
    }
}