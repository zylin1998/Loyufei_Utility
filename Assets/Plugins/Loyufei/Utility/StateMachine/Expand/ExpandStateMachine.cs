using System;
using System.Linq;
using System.Collections.Generic;

namespace Loyufei.StateMachine
{
    public class ExpandStateMachine : IExpandStateMachine
    {
        public ExpandStateMachine() : this(new StateMachineBase()) 
        {

        }

        public ExpandStateMachine(IStateMachine core) 
        {
            Core = core;
        }

        public bool ForceExit 
        {
            get => Core.ForceExit;

            set => Core.ForceExit = value;
        }

        public IStateMachine Core { get; }

        public IState Current => Core.Current;

        public IEnumerable<IState> States => Core.States;

        public virtual void Add(IState state) 
        {
            Core.Add(state);
        }

        public void SetState(IState state) 
        {
            Core.SetState(state);
        }

        public void SetState(object identity) 
        {
            var state = States.FirstOrDefault(s => Equals(s.Identity, identity));

            if (state.IsDefault()) { return; }

            SetState(state);
        }

        public virtual void Transfer() 
        {
            Core.Transfer();

            if (Current is IStateMachine machine) { machine.Transfer(); }
        }

        public void Tick() 
        {
            Core.Tick();
        }

        public void FixedTick()
        {
            Core.FixedTick();
        }

        public void LateTick()
        {
            Core.LateTick();
        }

        public virtual void Dispose() 
        {
            if (Core is IDisposable disposable) { disposable.Dispose(); }
        }
    }
}
