
namespace WebChat.Application.Cqrs.Friend.Responses
{
    public record FriendResponse
    {
        public int FriendShipId { get; set; }
        public string FriendName { get; set; }
        public string FriendId { get; set; }
        public string? imgUrl { get; set; }
    }
}
