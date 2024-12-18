using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei.StateMachine
{
    public abstract class StateAssetBase : ScriptableObject, IState
    {
        public abstract object Identity { get; }

        public abstract bool CanEnter { get; }

        public abstract bool CanExit  { get; }

        public virtual void FixedTick()
        {
            
        }

        public virtual void LateTick()
        {
            
        }

        public virtual void OnEnter()
        {
            
        }

        public virtual void OnExit()
        {
            
        }

        public virtual void Tick()
        {
            
        }
    }
}
