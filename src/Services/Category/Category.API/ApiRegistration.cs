using Category.Application.Features.ExpenseTypes.Search;
using Category.Application.Models.Search;
using Search.Service.Interfaces;
using Search.Service.Models;

namespace Category.API
{
    public static class ApiRegistration
    {
        public static IServiceCollection AddApiservices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISearchService<CategorySearchModel>, CategorySearchService>();
            services.Configure<ElasticSearchOptions>(configuration.GetSection(ElasticSearchOptions.SectionName));

            return services;
        }
    }
}
