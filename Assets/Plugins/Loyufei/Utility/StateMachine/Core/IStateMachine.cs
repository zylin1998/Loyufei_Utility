using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei.StateMachine
{
    public interface IStateMachine 
    {
        public bool ForceExit { get; set; }

        public IState Current { get; }

        public IEnumerable<IState> States { get; }

        public void Add(IState state);

        public void SetState(IState state);

        public void SetState(object state);

        public void Transfer();

        public void Tick();

        public void FixedTick();

        public void LateTick();
    }
}