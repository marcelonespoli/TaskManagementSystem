using System;
using TMS.Domain.Core.Events;

namespace TMS.Domain.Tasks.Events
{
    public abstract class BaseTaskEvent : Event
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public DateTime? StartDate { get; protected set; }
        public DateTime? FinishDate { get; protected set; }
        public int State { get; protected set; }
    }
}
