using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebChat.Application.Abstractions.IInterfaces.Services;
using WebChat.Application.Cqrs.Authentication.Service;
using WebChat.Application.Cqrs.Friend.Service;
using WebChat.Application.Cqrs.Groups.Service;
using WebChat.Application.Cqrs.Message.service;
using WebChat.Domain.Repositories;
using WebChat.Persistence.ContextData;
using WebChat.Persistence.Repositories;
using WebChat.Persistence.Services;
using WebChat.Presistance.Services;

namespace WebChat.Persistence.StartServices.ServiceCollection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddChatContext(configuration)
                    .AddScoped<IUnitOfWork, UnitOfWork>()
                    .AddScoped<IGroupService,GroupQueryService>()
                    .AddScoped<IFreindService,FriendQueryService>()
                    .AddScoped<IUserManagerService,UserManagerService>()
                    .AddScoped<IMessageService,MessageQueryService>()
                    .AddScoped<IAuthService,AuthService>();

            return services;
        }
        private static IServiceCollection AddChatContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ChatDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    o=>o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
             });
          
            return services;
        }

    }
}
