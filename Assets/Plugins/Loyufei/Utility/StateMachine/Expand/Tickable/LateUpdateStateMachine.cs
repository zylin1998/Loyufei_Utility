using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Loyufei.StateMachine
{
    internal class LateUpdateStateMachine : ExpandStateMachine, ITickableStateMachine
    {
        public LateUpdateStateMachine() : this(new StateMachineBase())
        {
            _CanDisable = () => false;
        }

        public LateUpdateStateMachine(IStateMachine core) : base(core)
        {
            _CanDisable = () => false;
        }

        private bool _Enable;

        private Func<bool> _CanDisable;

        private IObservable<long> _Observable;

        public ITickableStateMachine Enable()
        {
            _Enable = true;

            _Observable = Observable.EveryLateUpdate().TakeWhile(l => _Enable && !_CanDisable.Invoke());

            _Observable.Subscribe(l =>
            {
                Transfer();

                LateTick();
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
