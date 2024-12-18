using Loyufei.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.StateMachine
{
    internal class DefaultState : IState
    {
        public object Identity { get; } = "Default";

        public bool CanEnter => false;

        public bool CanExit  => true;

        public void OnEnter()
        {

        }

        public void OnExit()
        {

        }

        public void Tick()
        {

        }
        
        public void FixedTick()
        {
            
        }

        public void LateTick()
        {
            
        }

    }
}
