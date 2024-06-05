
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Friend.Commands.FriendRequest
{
    public record FriendRquestCommand(int friendShipId,bool confirmRequest,string friendId) :ICommand<BaseResponse>;
}
