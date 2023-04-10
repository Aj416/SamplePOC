using Category.Application.Contracts.Persistence;
using Category.Infrastructure.Host;
using Category.Infrastructure.Persistence;
using Category.Infrastructure.Repositories;
using Core.Service.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Category.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DbContext, CategoryDbContext>();
            services.AddDbContext<CategoryDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CategoryConnectionString"), providerOptions => providerOptions.EnableRetryOnFailure()));

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddHostedService<MigratorHostedService>();

            return services;
        }
    }
}
