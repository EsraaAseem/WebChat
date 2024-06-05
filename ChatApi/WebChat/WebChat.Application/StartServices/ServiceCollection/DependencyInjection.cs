using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebChat.Application.StartServices.Behaviors;

namespace WebChat.Application.StartServices.ServiceCollection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMapping().AddMediatr().AddPipline().AddValidatorPipline();
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);
            return services;
        }
        private static IServiceCollection AddMapping(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            services.AddScoped<IMapper, ServiceMapper>();

            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);
            return services;
        }
        private static IServiceCollection AddMediatr(this IServiceCollection services)
        {
         
          return  services.AddMediatR(config =>config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        } 
 
        private static IServiceCollection AddPipline(this IServiceCollection services)
        {
            return services.AddScoped(typeof(IPipelineBehavior<,>),typeof(LoggingPipelineBehavior<,>));
        }
        private static IServiceCollection AddValidatorPipline(this IServiceCollection services)
        {
            return services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        }
      /*  private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            return  services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }*/
    }
}
