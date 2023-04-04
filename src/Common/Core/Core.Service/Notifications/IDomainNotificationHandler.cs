using MediatR;

namespace Core.Service.Notifications
{
    public interface IDomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        IEnumerable<DomainNotification> GetNotifications();
        bool HasNotifications();
        void Dispose();
    }
}
