using Category.Application.Contracts.Persistence;
using Category.Domain.Entity;
using Category.Infrastructure.Persistence;
using Core.Service.Repositories;

namespace Category.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<ExpenseType>, ICategoryRepository
    {
        public CategoryRepository(CategoryDbContext context) : base(context) { }

    }
}
