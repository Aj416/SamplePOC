using Core.Service.Events;

namespace Core.Service.Notifications
{
    public class DomainNotification : Event
    {
        public Guid DomainNotificationId { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public int Version { get; private set; }
        public int Code { get; private set; }
        public DomainNotification(string key, string value)
        {
            Key = key;
            Value = value;
            DomainNotificationId = Guid.NewGuid();
            Version = 1;
        }
        public DomainNotification(string key, string value, int code)
        {
            Key = key;
            Value = value;
            DomainNotificationId = Guid.NewGuid();
            Version = 1;
            Code = code;
        }
    }
}
