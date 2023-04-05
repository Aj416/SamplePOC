using Core.Service.Behaviours;
using Core.Service.Bus;
using Core.Service.Events;
using Core.Service.Notifications;
using Core.Service.TimeStamp;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.Service
{
    public static class CoreServiceRegistration
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandValidationBehaviours<,>));
            services.AddScoped<IContextBehaviour, CreatedDateBehaviour>();
            services.AddScoped<IContextBehaviour, ModifiedDateBehaviour>();
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            services.AddScoped<IEventStore, InMemoryEventStore>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<IDomainNotificationHandler, DomainNotificationHandler>();
            services.AddScoped<ITimeStampService, TimeStampService>();

            return services;
        }
    }
}
