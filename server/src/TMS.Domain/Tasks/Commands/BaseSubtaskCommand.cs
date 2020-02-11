using System;
using TMS.Domain.Core.Commands;

namespace TMS.Domain.Tasks.Commands
{
    public abstract class BaseSubtaskCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public States State { get; protected set; }
        public Guid TaskId { get; protected set; }        
    }
}
