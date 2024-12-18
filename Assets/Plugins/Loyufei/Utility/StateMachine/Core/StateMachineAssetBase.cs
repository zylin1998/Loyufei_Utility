using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Loyufei.StateMachine
{
    public abstract class StateMachineAssetBase : ScriptableObject, IStateMachine
    {
        public abstract IEnumerable<IState> States { get; }

        public IState Current { get; protected set; } = IState.Default;

        public virtual bool ForceExit { get; set; }

        public abstract void FixedTick();
        public abstract void LateTick();
        public abstract void Tick();
        public abstract void Add(IState state);
        public abstract void SetState(IState state);
        public abstract void SetState(object identity);
        public abstract void Transfer();
    }
}
