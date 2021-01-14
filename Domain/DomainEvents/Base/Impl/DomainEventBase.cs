using MediatR;
using System;

namespace Domain.DomainEvents.Base.Impl
{
    public abstract class DomainEventBase : INotification    
    {
        public DateTime Date { get; private set; }
        public string EventTypeName { get; protected set; }
        public Guid EntityId { get; protected set; }

        protected DomainEventBase(Guid id)
        {
            EventTypeName = this.GetType().Name;
            EntityId = id;
            Date = DateTime.UtcNow;
        }
    }
}
