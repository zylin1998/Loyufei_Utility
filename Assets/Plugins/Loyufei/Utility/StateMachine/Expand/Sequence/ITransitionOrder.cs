using System.Collections;
using System.Collections.Generic;

namespace Loyufei.StateMachine
{
    public interface ITransitionOrder
    {
        public IEnumerable<object> Orders { get; }
    }
}