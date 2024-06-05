
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Authentication.Qureies.UserProfile
{
    public record UserProfileQuery(string userId):IQuery<BaseResponse>;
    
}
