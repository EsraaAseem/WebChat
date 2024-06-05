
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Groups.Qureies.GetGroupById
{
    public record GetGroupByIdQuery(int groupId):IQuery<BaseResponse>;
    
}
