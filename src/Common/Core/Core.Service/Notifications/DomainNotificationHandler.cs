namespace Core.Service.Notifications
{
    public class DomainNotificationHandler : IDomainNotificationHandler
    {
        private List<DomainNotification> _notifications;

        public DomainNotificationHandler() => _notifications = new List<DomainNotification>();
        public void Dispose() => _notifications = new List<DomainNotification>();
        public IEnumerable<DomainNotification> GetNotifications() => _notifications;
        public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
        {
            _notifications.Add(notification);
            return Task.CompletedTask;
        }
        public bool HasNotifications() => GetNotifications().Any();
    }
}
