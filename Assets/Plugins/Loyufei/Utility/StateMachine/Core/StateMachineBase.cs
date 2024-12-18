using System;
using System.Linq;
using System.Collections.Generic;

namespace Loyufei.StateMachine
{
    public class StateMachineBase : IStateMachine
    {
        private List<IState> _States = new();

        public IEnumerable<IState> States => _States;

        public IState Current { get; private set; } = IState.Default;
        
        public bool ForceExit { get; set; }

        public void Add(IState state)
        {
            _States.Add(state);
        }

        public void SetState(IState state)
        {
            Current.OnExit();

            Current = state ?? IState.Default;

            Current.OnEnter();
        }

        public void SetState(object identity) 
        {
            var state = States.FirstOrDefault(s => Equals(s.Identity, identity));

            if (state.IsDefault()) { return; }

            SetState(state);
        }

        public void Tick()
        {
            Current.Tick();
        }

        public void FixedTick()
        {
            Current.FixedTick();
        }

        public void LateTick()
        {
            Current.LateTick();
        }

        public virtual void Transfer()
        {
            if (!Current.CanExit && !ForceExit) { return; }

            var next = States.FirstOrDefault(state => state.CanEnter) ?? IState.Default;

            SetState(next);
        }
    }
}
