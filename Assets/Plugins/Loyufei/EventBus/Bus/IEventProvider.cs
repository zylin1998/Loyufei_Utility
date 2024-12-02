using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.DomainEvents
{
    public interface IEventProvider
    {
        public IEnumerable<IDomainEvent> ProvideEvents();
    }
}
