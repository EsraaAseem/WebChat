
namespace WebChat.Application.Cqrs.ShareResponse
{
    public sealed record GroupMembersCommand
    {
        public UserChatsResponse Group { get; set; }
        public List<string>? Users { get; set; }
    }
}
