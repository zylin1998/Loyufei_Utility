using System;
using System.Linq;
using System.Collections.Generic;

namespace Loyufei.StateMachine
{
    public interface IFunctionalState : IState
    {
        public IFunctionalState EnterWhen(Func<bool> condition);
        public IFunctionalState ExitWhen(Func<bool> condition);
        public IFunctionalState DoOnEnter(Action callBack);
        public IFunctionalState DoOnExit(Action callBack);
        public IFunctionalState DoTick(Action callBack);
        public IFunctionalState DoFixedTick(Action callBack);
        public IFunctionalState DoLateTick(Action callBack);
        public IFunctionalState WithIdentity(object identity);
    }
}
