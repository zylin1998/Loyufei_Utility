using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Loyufei.StateMachine
{
    internal class UpdateStateMachine : ExpandStateMachine, ITickableStateMachine
    {
        public UpdateStateMachine() : this(new StateMachineBase()) 
        {
            _CanDisable = () => false;
        }

        public UpdateStateMachine(IStateMachine core) : base(core)
        {
            _CanDisable = () => false;
        }

        private bool _Enable;

        private Func<bool> _CanDisable;

        private IObservable<long> _Observable;

        public ITickableStateMachine Enable() 
        {
            _Enable = true;

            _Observable = Observable.EveryUpdate().TakeWhile(l => _Enable && !_CanDisable.Invoke());

            _Observable.Subscribe(l =>
            {
                Transfer();

                Tick();
            }, Dispose);

            return this;
        }

        public ITickableStateMachine DisableWhen(Func<bool> predicate)
        {
            _CanDisable = predicate;

            return this;
        }
    }
}
