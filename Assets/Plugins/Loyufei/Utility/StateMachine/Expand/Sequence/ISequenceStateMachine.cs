using System;
using System.Linq;
using System.Collections.Generic;

namespace Loyufei.StateMachine
{
    public interface ISequenceStateMachine : IExpandStateMachine
    {
        public ITransitionOrder Order { get; set; }

        public bool Cycle  { get; }
        public bool Active { get; }

        public void Reset();
    }
}
