

namespace Category.API
{
    public static class ApiRegistrations
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddProblemDetails();

            return services;
        }
    }
}
