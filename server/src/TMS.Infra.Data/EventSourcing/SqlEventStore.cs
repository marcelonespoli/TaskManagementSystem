using Newtonsoft.Json;
using TMS.Domain.Core.Events;
using TMS.Domain.Interfaces;
using TMS.Infra.Data.Repository.EventSourcing;

namespace TMS.Infra.Data.EventSourcing
{
    public class SqlEventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        public SqlEventStore(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        public void SaveEvent<T>(T @event) where T : Event
        {
            var serializedData = JsonConvert.SerializeObject(@event);

            var storedEvent = new StoredEvent(
                @event,
                serializedData);

            _eventStoreRepository.Store(storedEvent);
        }
    }
}
