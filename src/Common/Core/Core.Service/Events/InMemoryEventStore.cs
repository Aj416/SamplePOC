namespace Core.Service.Events
{
    public class InMemoryEventStore : IEventStore
    {
        public void Save<T>(T theEvent) where T : Event
        {
            // throw new NotImplementedException();
        }
    }
}
