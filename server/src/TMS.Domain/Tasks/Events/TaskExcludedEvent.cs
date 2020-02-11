using System;

namespace TMS.Domain.Tasks.Events
{
    public class TaskExcludedEvent : BaseTaskEvent
    {
        public TaskExcludedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}
