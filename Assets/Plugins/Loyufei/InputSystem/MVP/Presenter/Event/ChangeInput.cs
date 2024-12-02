using System;
using System.Linq;
using System.Collections.Generic;
using Loyufei.DomainEvents;

namespace Loyufei
{
    public struct ChangeInput : IDomainEvent
    {
        public ChangeInput(object storageId, object inputId, EPositive positive)
            => (StorageId, InputId, Positive) = (storageId, inputId, positive);

        public object    StorageId { get; }
        public object    InputId   { get; }
        public EPositive Positive  { get; }
    }

    public struct ChangeInputMode : IDomainEvent
    {
        public ChangeInputMode(object mode)
        {
            Mode = mode;
        }

        public object Mode { get; }
    }
}
