using Core.Service.Domain;
using Core.Service.TimeStamp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Core.Service.Behaviours
{
    public class CreatedDateBehaviour : IContextBehaviour
    {
        private readonly ITimeStampService _timeStampService;

        public CreatedDateBehaviour(ITimeStampService timeStampService) => _timeStampService = timeStampService;
        public void Apply(IEnumerable<EntityEntry> entries)
        {
            entries.Where(x => x.State == EntityState.Added).Select(x => x.Entity).OfType<ICreatedDate>().ToList()
                .ForEach(x => x.CreatedDate = _timeStampService.GetUtcTimeStamp());
        }
    }
}
