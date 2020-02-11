using System;
using TMS.Domain.Core.Events;

namespace TMS.Domain.Tasks.Events
{
    public class BaseSubtaskEvent : Event
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public int State { get; protected set; }
        public Guid TaskId { get; protected set; }
    }
}
