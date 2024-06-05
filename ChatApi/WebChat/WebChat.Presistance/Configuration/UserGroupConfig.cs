using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebChat.Domain.Entities;

namespace WebChat.Persistence.Configuration
{
    public class UserGroupConfig : IEntityTypeConfiguration<UserGroup> 
    {
       public void Configure(EntityTypeBuilder<UserGroup> builder)
       {
            builder.HasKey(ug => new { ug.UserId, ug.GroupUserId });
            builder.HasOne(ug => ug.Users)
            .WithMany(g=>g.UserGroups)
            .HasForeignKey(ug => ug.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(ug => ug.GroupUser)
            .WithMany(g=>g.UserGroups)
            .HasForeignKey(ug => ug.GroupUserId).OnDelete(DeleteBehavior.Cascade);
       }
    }
}
