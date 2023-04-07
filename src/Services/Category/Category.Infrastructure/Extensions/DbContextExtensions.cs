using Category.Domain.Entity;
using Category.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Category.Infrastructure.Extensions
{
    public static class DbContextExtensions
    {
        public static async Task SeedData(this CategoryDbContext dbContext)
        {
            if (dbContext.AllMigrationApplied())
            {
                if (!await dbContext.ExpenseTypes.AnyAsync())
                {
                    dbContext.ExpenseTypes.AddRange(AddPreconfiguredData());

                    if (dbContext.ChangeTracker.HasChanges())
                    {
                        await dbContext.SaveChangesAsync();
                    }
                }
            }
        }

        private static IEnumerable<ExpenseType> AddPreconfiguredData()
        {
            return new List<ExpenseType> {
                new ExpenseType() { Name = "Others", Description = "Miscallaneous expenses"}
            };
        }

        private static bool AllMigrationApplied(this CategoryDbContext dbContext)
        {
            var applied = dbContext.GetService<IHistoryRepository>().GetAppliedMigrations().Select(x => x.MigrationId);

            var total = dbContext.GetService<IMigrationsAssembly>().Migrations.Select(x => x.Key);

            return !total.Except(applied).Any();
        }
    }
}
