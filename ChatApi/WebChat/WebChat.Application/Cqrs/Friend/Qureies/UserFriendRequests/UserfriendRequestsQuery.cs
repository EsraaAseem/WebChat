
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Friend.Qureies.UserFriendRequests
{
    public record UserfriendRequestsQuery(string userId):IQuery<BaseResponse>;
}
