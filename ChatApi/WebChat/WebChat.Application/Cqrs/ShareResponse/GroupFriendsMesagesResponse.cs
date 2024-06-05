

namespace WebChat.Application.Cqrs.ShareResponse
{
    public record GroupFriendsMesagesResponse (int messageId, string content, DateTime messageTime)
    {
    }
}
