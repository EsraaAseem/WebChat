
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Authentication.Qureies.Login
{
    public record LoginQuery(string userName,string password):IQuery<BaseResponse>;
   
}
