

namespace WebChat.Application.Cqrs.ShareResponse
{
    public record FriendMessageResponse
    {
        public int MessageId { get; set; }
        public string SenderId { get; set; }
        public string Content { get; set; }
        public int IsDeleteBySender { get; set; }
        public int IsDeleteByReciver { get; set; }
        public string MessageTime { get; set; }
        public bool IsRead { get; set; }
    }
}
