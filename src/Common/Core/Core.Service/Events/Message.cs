using MediatR;

namespace Core.Service.Events
{
    public abstract class Message : INotification
    {
        public string MessageType { get; protected set; }
        public Message()
        {
            MessageType = GetType().Name;
        }
    }
}
