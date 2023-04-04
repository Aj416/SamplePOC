using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Core.Service.Behaviours
{
    public interface IContextBehaviour
    {
        void Apply(IEnumerable<EntityEntry> entries);
    }
}
