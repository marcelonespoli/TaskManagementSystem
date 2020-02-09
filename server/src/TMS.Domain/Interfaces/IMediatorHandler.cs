using System.Threading.Tasks;
using TMS.Domain.Core.Commands;
using TMS.Domain.Core.Events;

namespace TMS.Domain.Interfaces
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T @event) where T : Event;
        Task SendCommand<T>(T command) where T : Command;
    }
}
