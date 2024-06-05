
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Groups.Qureies.GetUserGroups
{
    public record GetUserGroupsQuery(string userId):IQuery<BaseResponse>;
}
