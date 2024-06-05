
using WebChat.Domain.Entities;

namespace WebChat.Application.Cqrs.ShareResponse
{
    public class GroupMessageResponse
    {
        public int MessageId { get; set; }
        public string SenderId { get; set; }
        public string Content { get; set; }
        public string SenderName { get; set; }
        public string SenderImgUrl { get; set; }
        public int IsDeleteBySender { get; set; }
        public int IsDeleteByReciver { get; set; }
        public DateTime MessageTime { get; set; }
        public IReadOnlyCollection<DeletedMessages>? DeletedForUserIds { get; set; }
    }
}
