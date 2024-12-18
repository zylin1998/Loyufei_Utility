using System;
using System.Linq;
using System.Collections.Generic;

namespace Loyufei.StateMachine
{
    public interface ITickableStateMachine : IExpandStateMachine, IDisposable
    {
        public ITickableStateMachine Enable();

        public ITickableStateMachine DisableWhen(Func<bool> predicate);
    }

    [Flags]
    public enum ETickMode 
    {
        None      = 0,
        Tick      = 1,
        FixedTick = 2,
        LateTick  = 4,
    }
}
