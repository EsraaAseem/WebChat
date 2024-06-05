

using WebChat.Application.Cqrs.ShareResponse;

namespace WebChat.Application.Cqrs.Friend.Qureies.UserChats
{
    public record UserChatsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? imgUrl { get; set; }
        public string Type { get; set; }
        public GroupFriendsMesagesResponse? Message { get; set; }
    }
}
