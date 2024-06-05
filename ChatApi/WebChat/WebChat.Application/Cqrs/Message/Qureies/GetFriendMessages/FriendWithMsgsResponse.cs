

using WebChat.Application.Cqrs.ShareResponse;

namespace WebChat.Application.Cqrs.Message.Qureies.GetFriendMessages
{
    public record FriendWithMsgsResponse
    {
        public int FriendShipId { get; set; }
        public string FriendId { get; set; }
        public string SenderName { get; set; }
        public string SenderImgUrl { get; set; }
        public List<FriendMessageResponse>? FriendsChat { get; set; }
    }
}
