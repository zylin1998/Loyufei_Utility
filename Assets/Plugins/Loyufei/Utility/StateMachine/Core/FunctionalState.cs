using System;
using System.Linq;
using System.Collections.Generic;

namespace Loyufei.StateMachine
{
    internal class FunctionalState : IFunctionalState
    {
        public FunctionalState() 
        {
            _Identity  = IIdentity.NULLId;
            _CanEnter  = () => false;
            _CanExit   = () => true;
            _OnEnter   = EmptyAction;
            _OnExit    = EmptyAction;
            _Tick      = EmptyAction;
            _FixedTick = EmptyAction;
            _LateTick  = EmptyAction;
        }

        private object _Identity;
        private Func<bool> _CanEnter;
        private Func<bool> _CanExit;
        private Action _OnEnter;
        private Action _OnExit;
        private Action _Tick;
        private Action _FixedTick;
        private Action _LateTick;

        public bool CanEnter => _CanEnter.Invoke();
        public bool CanExit  => _CanExit.Invoke();

        public object Identity => _Identity;

        public IFunctionalState EnterWhen(Func<bool> condition) 
        {
            _CanEnter = condition;

            return this;
        }

        public IFunctionalState ExitWhen(Func<bool> condition)
        {
            _CanExit = condition;

            return this;
        }

        public IFunctionalState DoOnEnter(Action callBack)
        {
            _OnEnter = callBack;
            
            return this;
        }

        public IFunctionalState DoOnExit(Action callBack)
        {
            _OnExit = callBack;

            return this;
        }

        public IFunctionalState DoTick(Action callBack)
        {
            _Tick = callBack;

            return this;
        }

        public IFunctionalState DoFixedTick(Action callBack)
        {
            _FixedTick = callBack;

            return this;
        }

        public IFunctionalState DoLateTick(Action callBack)
        {
            _LateTick = callBack;

            return this;
        }

        public IFunctionalState WithIdentity(object identity) 
        {
            _Identity = identity;

            return this;
        }

        public void OnEnter() 
        {
            _OnEnter.Invoke();
        }

        public void OnExit()
        {
            _OnExit.Invoke();
        }

        public void Tick() 
        {
            _Tick.Invoke();
        }

        public void FixedTick()
        {
            _FixedTick.Invoke();
        }

        public void LateTick()
        {
            _LateTick.Invoke();
        }

        private static void EmptyAction() 
        {

        }
    }
}
