using Core.Service.Domain;
using Core.Service.TimeStamp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Core.Service.Behaviours
{
    public class ModifiedDateBehaviour : IContextBehaviour
    {
        private readonly ITimeStampService _timeStampService;

        public ModifiedDateBehaviour(ITimeStampService timeStampService) => _timeStampService = timeStampService;

        public void Apply(IEnumerable<EntityEntry> entries)
        {
            entries.Where(x => x.State == EntityState.Added || x.State == EntityState.Modified)
                .Select(x => x.Entity).OfType<IModifiedDate>().ToList()
                .ForEach(x => x.ModifiedDate = _timeStampService.GetUtcTimeStamp());
        }
    }
}
