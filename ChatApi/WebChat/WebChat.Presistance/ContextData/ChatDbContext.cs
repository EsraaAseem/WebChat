using Microsoft.EntityFrameworkCore;
using WebChat.Domain.Entities;
using WebChat.Persistence.Configuration;

namespace WebChat.Persistence.ContextData
{
    public class ChatDbContext :DbContext
    {
        public ChatDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<FriendShip> FriendShip { get; set; }

      //public DbSet<FriendMessages> Messages { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<GroupMessages> GroupMessages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(FriendConfig).Assembly);
        }
    }
}
