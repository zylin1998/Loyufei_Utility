using System;
using System.Linq;
using System.Collections.Generic;
using Loyufei.DomainEvents;

namespace Loyufei
{
    public struct UpdateInput : IDomainEvent
    {
        public UpdateInput(object storageId, object inputId, EInputKey key, EPositive positive)
            => (StorageId, InputId, Key, Positive) = (storageId, inputId, key, positive);

        public object    StorageId { get; }
        public object    InputId   { get; }
        public EInputKey Key       { get; }
        public EPositive Positive  { get; }
    }
}
