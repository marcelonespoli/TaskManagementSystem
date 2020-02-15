using System;
using TMS.Domain.Core.Commands;

namespace TMS.Domain.Tasks.Commands
{
    public abstract class BaseTaskCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public DateTime? StartDate { get; protected set; }
        public DateTime? FinishDate { get; protected set; }
        public States State { get; set; }        
    }
}
