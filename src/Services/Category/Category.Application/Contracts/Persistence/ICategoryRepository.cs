using Category.Domain.Entity;
using Core.Service.Repositories;

namespace Category.Application.Contracts.Persistence
{
    public interface ICategoryRepository : IRepository<ExpenseType>
    {
    }
}
