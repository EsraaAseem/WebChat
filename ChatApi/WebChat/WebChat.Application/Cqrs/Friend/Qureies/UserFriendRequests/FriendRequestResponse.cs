
namespace WebChat.Application.Cqrs.Friend.Qureies.UserFriendRequests
{
    public record FriendRequestResponse
    {
        public int FriendShipId { get; set; }
        public string SenderName { get; set; }
        public string FriendRequestSenderId { get; set; }
        public string FriendRequestReciverId { get; set; }
        public string imgUrl { get; set; }
    }
}
