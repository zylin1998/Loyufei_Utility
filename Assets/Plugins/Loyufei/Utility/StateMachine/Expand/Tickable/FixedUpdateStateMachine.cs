using System;
using System.Linq;
using System.Collections.Generic;
using UniRx;

namespace Loyufei.StateMachine
{
    internal class FixedUpdateStateMachine : ExpandStateMachine, ITickableStateMachine
    {
        public FixedUpdateStateMachine() : this(new StateMachineBase())
        {
            _CanDisable = () => false;
        }

        public FixedUpdateStateMachine(IStateMachine core) : base(core)
        {
            _CanDisable = () => false;
        }

        private bool _Enable;

        private IObservable<long> _Observable;

        private Func<bool> _CanDisable;

        public ITickableStateMachine Enable()
        {
            _Enable = true;

            _Observable = Observable.EveryFixedUpdate().TakeWhile(l => _Enable && !_CanDisable.Invoke());

            _Observable.Subscribe(l =>
            {
                Transfer();

                FixedTick();
            }, Dispose);

            return this;
        }

        public ITickableStateMachine DisableWhen(Func<bool> predicate) 
        {
            _CanDisable = predicate;

            return this;
        }

        public override void Dispose()
        {
            _Enable = false;

            base.Dispose();
        }
    }
}
