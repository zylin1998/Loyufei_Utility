using System;
using System.Collections.Generic;
using System.Linq;

namespace Loyufei.StateMachine
{
    public interface IExpandStateMachine : IStateMachine, IDisposable
    {
        public IStateMachine Core { get; }
    }
}
