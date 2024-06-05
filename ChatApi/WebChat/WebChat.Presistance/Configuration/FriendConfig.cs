using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebChat.Domain.Entities;

namespace WebChat.Persistence.Configuration
{
    public class FriendConfig : IEntityTypeConfiguration<FriendShip>
    {
            public void Configure(EntityTypeBuilder<FriendShip> builder)
            {
                builder.HasKey(f=>f.FriendshipId);
               builder.HasOne(f => f.User)
                .WithMany()
               .HasForeignKey(f => f.UserId)
              .OnDelete(DeleteBehavior.Restrict);

                builder
                .HasOne(f => f.Friend)
                .WithMany()
                .HasForeignKey(f => f.FriendId)
                .OnDelete(DeleteBehavior.Restrict);

            // builder.HasOne(f=>f.AppUser).WithMany().HasForeignKey(dr => dr.UserId).OnDelete(DeleteBehavior.NoAction);
            }
    }
}
