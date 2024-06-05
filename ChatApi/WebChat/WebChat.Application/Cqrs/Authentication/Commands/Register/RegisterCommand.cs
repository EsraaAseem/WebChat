
using Microsoft.AspNetCore.Http;
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Authentication.Commands.Register
{
    public record RegisterCommand(string userName,
        string password,
        string name,
        string phoneNumber,
        IFormFile userImg):ICommand<BaseResponse>;
    
}
