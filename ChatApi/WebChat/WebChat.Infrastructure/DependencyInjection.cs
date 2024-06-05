

using Microsoft.Extensions.DependencyInjection;
using WebChat.Application.Abstractions.IInterfaces.Services;
using WebChat.Infrastructure.Services.AuthService;
using WebChat.Infrastructure.Services.MediaService;

namespace WebChat.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IMediaService, MediaService>()
                .AddScoped<IPasswordHashService,PasswordHashService>()
            .AddScoped<IJwtService, JwtService>();
            return services;
        }
    }
}
