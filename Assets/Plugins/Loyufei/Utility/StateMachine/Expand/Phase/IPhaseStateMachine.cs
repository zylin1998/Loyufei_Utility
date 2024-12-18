using System;
using System.Linq;
using System.Collections.Generic;

namespace Loyufei.StateMachine
{
    public interface IPhaseStateMachine : IExpandStateMachine, IState
    {
        public IPhaseStateMachine EnterWhen(Func<bool> condition);
        public IPhaseStateMachine ExitWhen(Func<bool> condition);
        public IPhaseStateMachine DoOnEnter(Action callBack);
        public IPhaseStateMachine DoOnExit(Action callBack);
        public IPhaseStateMachine WithIdentity(object identity);
    }
}
