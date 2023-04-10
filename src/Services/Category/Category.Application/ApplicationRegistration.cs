using Category.Application.Behaviours;
using Category.Application.Features.ExpenseTypes.Search;
using Category.Application.Models.Search;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Search.Service.Interfaces;
using Search.Service.Models;
using System.Reflection;

namespace Category.Application
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ElasticSearchOptions>(configuration.GetSection(ElasticSearchOptions.SectionName));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddScoped<ISearchService<CategorySearchModel>, CategorySearchService>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            return services;
        }
    }
}
