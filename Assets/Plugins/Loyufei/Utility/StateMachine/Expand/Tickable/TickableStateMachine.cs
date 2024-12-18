using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Loyufei.StateMachine
{
    internal class TickableStateMachine : ExpandStateMachine, ITickableStateMachine
    {
        public TickableStateMachine(ETickMode tickMode) : this(new StateMachineBase(), tickMode) 
        {
            _CanDisable = () => false;
        }

        public TickableStateMachine(IStateMachine core, ETickMode tickMode) : base(core)
        {
            _TickMode = tickMode;

            _CanDisable = () => false;
        }

        private bool _Enable;
        private ETickMode _TickMode;

        private Func<bool> _CanDisable;

        private IObservable<long> _Tick;
        private IObservable<long> _FixedTick;
        private IObservable<long> _LateTick;

        public ITickableStateMachine Enable()
        {
            _Enable = true;

            _Tick = Observable.EveryUpdate().TakeWhile(l => _Enable && !_CanDisable.Invoke());

            _Tick.Subscribe(l => Transfer(), Dispose);
            
            if (_TickMode.HasFlag(ETickMode.Tick)) 
            {
                _Tick.Subscribe(l => Tick()); 
            }
            
            if (_TickMode.HasFlag(ETickMode.FixedTick)) 
            {
                _FixedTick = Observable.EveryFixedUpdate().TakeWhile(l => _Enable && !_CanDisable.Invoke());

                _FixedTick.Subscribe(l => FixedTick()); 
            }

            if (_TickMode.HasFlag(ETickMode.LateTick)) 
            {
                _LateTick = Observable.EveryLateUpdate().TakeWhile(l => _Enable && !_CanDisable.Invoke());

                _LateTick.Subscribe(l => LateTick()); 
            }

            return this;
        }

        public ITickableStateMachine DisableWhen(Func<bool> predicate)
        {
            _CanDisable = predicate;

            return this;
        }
    }
}
