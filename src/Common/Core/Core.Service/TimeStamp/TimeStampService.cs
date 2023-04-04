namespace Core.Service.TimeStamp
{
    public class TimeStampService : ITimeStampService
    {
        private readonly DateTime _utcTimeStamp = DateTime.UtcNow;

        public DateTime GetUtcTimeStamp() => _utcTimeStamp;
    }
}
