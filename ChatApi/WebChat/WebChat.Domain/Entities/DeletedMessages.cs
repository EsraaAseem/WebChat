using Microsoft.EntityFrameworkCore;

namespace WebChat.Domain.Entities
{
    [Owned]
    public class DeletedMessages
    {
        public DeletedMessages(string userId)
        {
            UserId = userId;
        }

        public string UserId { get;private set; }
    }
}
