using Core.Service.Behaviours;
using Core.Service.Bus;
using Core.Service.Events;
using Core.Service.Extensions;
using Core.Service.Notifications;
using Core.Service.TimeStamp;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.Service
{
    public static class CoreServiceRegistration
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddScoped<IDomainNotificationHandler, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<DomainNotification>>(x => x.GetService<IDomainNotificationHandler>());
            services.AddScoped<IEventStore, InMemoryEventStore>();
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CommandValidationBehaviours<,>));
            services.AddScoped<ITimeStampService, TimeStampService>();
            services.AddScoped<IContextBehaviour, CreatedDateBehaviour>();
            services.AddScoped<IContextBehaviour, ModifiedDateBehaviour>();
           

            return services;
        }
    }
}
