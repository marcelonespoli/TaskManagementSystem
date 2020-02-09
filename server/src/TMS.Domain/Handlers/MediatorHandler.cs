using MediatR;
using System.Threading.Tasks;
using TMS.Domain.Core.Commands;
using TMS.Domain.Core.Events;
using TMS.Domain.Interfaces;

namespace TMS.Domain.Handlers
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IEventStore _eventStore;
        private readonly IMediator _mediator;

        public MediatorHandler(IEventStore eventStore, IMediator mediator)
        {
            _eventStore = eventStore;
            _mediator = mediator;
        }

        public async Task SendCommand<T>(T command) where T : Command
        {
            await _mediator.Send(command);
        }

        public async Task PublishEvent<T>(T @event) where T : Event
        {
            if (!@event.MessageType.Equals("DomainNotification"))
                _eventStore.SaveEvent(@event);

            await _mediator.Publish(@event);
        }
    }
}
