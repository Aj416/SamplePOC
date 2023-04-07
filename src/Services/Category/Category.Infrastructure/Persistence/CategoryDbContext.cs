using Category.Domain.Entity;
using Category.Infrastructure.Configuration;
using Core.Service.Behaviours;
using Microsoft.EntityFrameworkCore;

namespace Category.Infrastructure.Persistence
{
    public class CategoryDbContext : DbContext
    {
        private readonly List<IContextBehaviour> _contextBehaviours = new();
        public CategoryDbContext(DbContextOptions<CategoryDbContext> options) : base(options) { }

        public CategoryDbContext(DbContextOptions<CategoryDbContext> options, IEnumerable<IContextBehaviour> contextBehaviours) : base(options)
        {
            _contextBehaviours = contextBehaviours.ToList();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var contextEntries = ChangeTracker.Entries().ToList();

            _contextBehaviours.ForEach(e => e.Apply(contextEntries));

            return await base.SaveChangesAsync(cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExpenseTypeConfiguration).Assembly);
        }

        public DbSet<ExpenseType> ExpenseTypes { get; set; }
    }
}
