using System;

namespace TMS.Domain.Tasks.Events
{
    public class SubtaskExcludedEvent : BaseSubtaskEvent
    {
        public SubtaskExcludedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}
