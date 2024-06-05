
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Friend.Qureies.UserFriendShips
{
    public record UserFriendShipQuery(string userId):IQuery<BaseResponse>;
}
