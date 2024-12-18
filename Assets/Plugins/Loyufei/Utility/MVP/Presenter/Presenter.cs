using System;
using System.Collections;
using System.Collections.Generic;
using Loyufei.DomainEvents;

namespace Loyufei.MVP
{
    public class Presenter : AggregateRoot
    {
        public virtual object GroupId { get; }

        protected override void Construct(IDomainEventBus eventBus)
        {
            base.Construct(eventBus);
            
            RegisterEvents();
        }

        protected virtual void RegisterEvents() 
        {

        }

        protected void SettleEvents(params IDomainEvent[] events) 
        {
            AggregateRootExtensions.SettleEvents(this, GroupId, events);
        }

        protected void SettleEvents(object groupId, params IDomainEvent[] events)
        {
            AggregateRootExtensions.SettleEvents(this, groupId, events);
        }

        protected void SettleEvents(IEnumerable<IDomainEvent> events)
        {
            AggregateRootExtensions.SettleEvents(this, events, GroupId);
        }

        protected void SettleEvents(object groupId, IEnumerable<IDomainEvent> events)
        {
            AggregateRootExtensions.SettleEvents(this, events, groupId);
        }

        protected void Register<TDomainEvent>(Action<TDomainEvent> callBack, bool priority = false) where TDomainEvent : IDomainEvent
        {
            EventBus.Register(callBack, GroupId, priority);
        }

        protected void Register<TDomainEvent>(object groupId, Action<TDomainEvent> callBack, bool priority = false) where TDomainEvent : IDomainEvent
        {
            EventBus.Register(callBack, groupId, priority);
        }
    }
}