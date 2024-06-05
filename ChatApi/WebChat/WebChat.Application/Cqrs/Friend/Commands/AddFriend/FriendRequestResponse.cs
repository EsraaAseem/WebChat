

namespace WebChat.Application.Cqrs.Friend.Commands.AddFriend
{
    public record FriendRequestResponse(int friendShipId,
        string senderName, string friendRequestSenderId, 
        string friendRequestReciverId, string imgUrl);
}
