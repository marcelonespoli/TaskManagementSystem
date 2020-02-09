using System;

namespace TMS.Domain.Core.Events
{
    public class StoredEvent : Event
    {
        public Guid Id { get; private set; }
        public string Data { get; private set; }

        // EF Constructor
        protected StoredEvent() { }

        public StoredEvent(Event task, string data)
        {
            Id = Guid.NewGuid();
            AggregateId = task.AggregateId;
            MessageType = task.MessageType;
            Data = data;
        }
    }
}
