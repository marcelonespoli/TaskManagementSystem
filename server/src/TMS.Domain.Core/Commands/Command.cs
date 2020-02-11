using MediatR;
using System;
using TMS.Domain.Core.Events;

namespace TMS.Domain.Core.Commands
{
    public abstract class Command: Message, IRequest<bool>
    {
        public DateTime Timestamp { get; private set; }

        public Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}
