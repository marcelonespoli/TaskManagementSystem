using TMS.Domain.Core.Events;

namespace TMS.Domain.Interfaces
{
    public interface IEventStore
    {
        void SaveEvent<T>(T @event) where T : Event;
    }
}
