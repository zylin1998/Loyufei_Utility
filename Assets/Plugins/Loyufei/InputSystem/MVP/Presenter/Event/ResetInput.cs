using Loyufei.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei
{
    public struct ResetInput : IDomainEvent
    {
        public ResetInput(object storageId)
        {
            StorageId = storageId;
        }

        public object StorageId { get; }
    }
}
