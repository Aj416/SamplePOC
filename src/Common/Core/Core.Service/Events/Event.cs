namespace Core.Service.Events
{
    public abstract class Event : Message
    {
        public DateTime TimeStamp { get; protected set; }
        public Event()
        {
            TimeStamp = DateTime.UtcNow;
        }
    }
}
