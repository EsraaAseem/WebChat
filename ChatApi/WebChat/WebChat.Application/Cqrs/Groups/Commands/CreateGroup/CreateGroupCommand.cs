using Microsoft.AspNetCore.Http;
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Groups.Commands.CreateGroup
{
    public sealed record CreateGroupCommand
    (
      string GroupName ,
     string CreatedGroupBy,
     IFormFile groupimg ,
     List<string>? Users 
   ):ICommand<BaseResponse>;
}
