
namespace WebChat.Domain.Entities
{
    public class AppUser
    {
        public AppUser(string userName,string phoneNumber,string imgUrl,string name)
        {
            UserName = userName;
            PhoneNumber = phoneNumber;
            ImgUrl = imgUrl;
            Name = name;
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserName { get; private set; }
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public string PhoneNumber { get; private set; }
        public string Name { get; private set; }
        public string ImgUrl { get; private set; }
        public virtual ICollection<AppUser> Friends { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
