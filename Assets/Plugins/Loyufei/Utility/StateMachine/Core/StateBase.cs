using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.StateMachine
{
    public abstract class StateBase : IState
    {
        public abstract object Identity { get; }

        public abstract bool CanEnter { get; }

        public abstract bool CanExit  { get; }

        public virtual void OnEnter()
        {
            
        }

        public virtual void OnExit()
        {
            
        }

        public virtual void Tick()
        {
            
        }

        public virtual void FixedTick()
        {

        }

        public virtual void LateTick()
        {

        }
    }
}
