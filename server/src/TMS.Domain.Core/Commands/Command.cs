using MediatR;
using System;
using TMS.Domain.Core.Events;

namespace TMS.Domain.Core.Commands
{
    public class Command: Message, IRequest
    {
        public DateTime Timestamp { get; private set; }

        public Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}
