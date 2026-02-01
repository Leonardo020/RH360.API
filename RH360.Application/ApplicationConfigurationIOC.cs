using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RH360.Application.Behaviors;

namespace RH360.Application
{
    public static class ApplicationConfigurationIOC
    {
        public static IServiceCollection AddCQRS(this IServiceCollection services)
        {
            // add mediatR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ApplicationConfigurationIOC).Assembly);
            });

            // add validators
            services.AddValidatorsFromAssembly(typeof(ApplicationConfigurationIOC).Assembly);

            // pipeline behaviors
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            return services;
        }
    }
}
