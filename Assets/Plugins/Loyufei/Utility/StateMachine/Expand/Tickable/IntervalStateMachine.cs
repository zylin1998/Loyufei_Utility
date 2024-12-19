using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Loyufei.StateMachine
{
    internal class IntervaStateMachine : ExpandStateMachine, ITickableStateMachine
    {
        public IntervaStateMachine(TimeSpan timeSpan) : base()
        {
            _TimeSpan = timeSpan;

            _CanDisable = () => false;
        }

        public IntervaStateMachine(IStateMachine core, TimeSpan timeSpan) : base(core)
        {
            _TimeSpan = timeSpan;

            _CanDisable = () => false;
        }

        private TimeSpan _TimeSpan;

        private bool _Enable;

        private Func<bool> _CanDisable;

        private IObservable<long> _Observable;

        public ITickableStateMachine Enable()
        {
            _Enable = true;

            _Observable = Observable.Interval(_TimeSpan).TakeWhile(l => _Enable && !_CanDisable.Invoke());

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

        public override void Dispose()
        {
            _Enable = false;

            base.Dispose();
        }
    }
}
