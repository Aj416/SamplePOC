using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Category.Infrastructure.Host
{
    /// <summary>
    /// Responsible for running DB migration and seeding asynchrounsly at startup.
    /// </summary>
    public class MigratorHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public MigratorHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Create a new scope to retrieve scoped services
            using (var scope = _serviceProvider.CreateScope())
            {


                // Run the database migration automatically for local and development environments
                var serviceScopeFactory = scope.ServiceProvider.GetRequiredService<IServiceScopeFactory>();
                await NativeInjectorBootStrapper.MigrateDatabase(serviceScopeFactory);
                await NativeInjectorBootStrapper.EnsureSeedData(serviceScopeFactory);

            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }


}
