using System;
using System.Linq;
using System.Collections.Generic;

namespace Loyufei.StateMachine
{
    internal class SequenceStateMachine : ExpandStateMachine, ISequenceStateMachine
    {
        public SequenceStateMachine() : base() 
        {

        }

        public SequenceStateMachine(IStateMachine core) : base(core)
        {

        }

        public SequenceStateMachine(IStateMachine core, ITransitionOrder order) : base(core)
        {
            Order = order;
        }

        private ITransitionOrder _Order;

        private IState[] _OrderedStates;

        public ITransitionOrder Order 
        {
            get => _Order;

            set 
            {
                _Order = value;

                _OrderedStates = Ordered().ToArray();
            }
        }

        public bool Cycle  { get; set; } = false;
        public bool Active 
        {
            get 
            {
                if (Flag < _OrderedStates.Length - 1) { return true; }

                return !Current.CanExit;
            }
        }
        
        public int Flag { get; private set; } = -1;

        public override void Add(IState state)
        {
            base.Add(state);

            _OrderedStates = Ordered().ToArray();
        }

        public override void Transfer()
        {
            if (!Current.CanExit && !ForceExit) { return; }

            ++Flag;
            
            if (Flag >= _OrderedStates.Length) 
            {
                if (Cycle) { Reset(); }

                else { SetState(IState.Default); return; }
            }

            SetState(_OrderedStates[Flag]);
        }

        public virtual void Reset() 
        {
            Flag = -1;
        }

        public override void Dispose() 
        {
            Reset();

            base.Dispose();
        }

        protected IEnumerable<IState> Ordered() 
        {
            var dic = States.ToDictionary(k => k.Identity);

            foreach (var order in Order.Orders) 
            {
                if (!dic.TryGetValue(order, out var state)) { continue; }

                yield return state;
            }
        }
    }
}
