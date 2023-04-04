using Core.Service.Commands;
using Core.Service.Events;
using Core.Service.Queries;

namespace Core.Service.Bus
{
    public interface IMediatorHandler
    {
        Task<T> SendCommand<T>(Command<T> command);
        Task SendCommand(Command command);
        Task RaiseEvent<T>(T thisEvent) where T : Event;
        Task<T> SendQuery<T>(Query<T> query);
        Task SendQuery(Query query);
    }
}
