using Category.Infrastructure.Extensions;
using Category.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Category.Infrastructure.Host
{
    public class NativeInjectorBootStrapper
    {
        public static async Task MigrateDatabase(IServiceScopeFactory serviceScopeFactory)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<NativeInjectorBootStrapper>>();

            var database = scope.ServiceProvider.GetService<CategoryDbContext>().Database;
            logger.LogInformation($"Running system migrations for database {database.GetDbConnection().Database}");
            await database.MigrateAsync();
        }

        public static async Task EnsureSeedData(IServiceScopeFactory serviceScopeFactory)
        {
            using var scope = serviceScopeFactory.CreateScope();

            await DbContextExtensions.SeedData(scope.ServiceProvider.GetService<CategoryDbContext>());


        }


    }
}
