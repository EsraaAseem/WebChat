
namespace WebChat.Domain.Entities
{
    public class UserGroup
    {
        public UserGroup(string?userId)
        {
            UserId = userId;
        }

        public string UserId { get; private set; }
        public AppUser Users { get; set; }
        public int GroupUserId { get; private set; }
        public Group GroupUser { get; set; }
    }
}
