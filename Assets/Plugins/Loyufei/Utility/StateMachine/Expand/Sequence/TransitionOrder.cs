using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.StateMachine
{
    public class TransitionOrder : ITransitionOrder
    {
        public TransitionOrder(IEnumerable<object> orders) 
        {
            _Orders = orders.ToArray();
        }

        public object[] _Orders;

        public IEnumerable<object> Orders => _Orders;
    }
}
