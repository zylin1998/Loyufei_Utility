using System;
using System.Linq;
using System.Collections.Generic;

namespace Loyufei.StateMachine
{
    internal class PhaseStateMachine : ExpandStateMachine, IPhaseStateMachine
    {
        public PhaseStateMachine() : base() 
        {

        }

        public PhaseStateMachine(IStateMachine core) : base(core) 
        {

        }

        private Func<bool> _CanEnter = () => false;
        private Func<bool> _CanExit  = () => true;

        private Action _OnEnter = () => { };
        private Action _OnExit  = () => { };

        public object Identity { get; private set; } = IIdentity.NULLId;
        public bool CanEnter => _CanEnter.Invoke();
        public bool CanExit  => _CanExit .Invoke();

        public IPhaseStateMachine EnterWhen(Func<bool> condition) 
        {
            _CanEnter = condition;

            return this;
        }

        public IPhaseStateMachine ExitWhen(Func<bool> condition) 
        {
            _CanExit  = condition;

            return this;
        }

        public IPhaseStateMachine DoOnEnter(Action callBack)
        {
            _OnEnter = callBack;

            return this;
        }

        public IPhaseStateMachine DoOnExit(Action callBack)
        {
            _OnExit = callBack;

            return this;
        }

        public IPhaseStateMachine WithIdentity(object identity)
        {
            Identity = identity;

            return this;
        }

        public void OnEnter() 
        {
            _OnEnter.Invoke();
        }

        public virtual void OnExit()
        {
            _OnExit.Invoke();
        }
    }
}
