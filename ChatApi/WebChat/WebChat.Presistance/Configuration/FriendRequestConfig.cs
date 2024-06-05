using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebChat.Domain.Entities;

namespace WebChat.Persistence.Configuration
{
    public class FriendRequestConfig : IEntityTypeConfiguration<FriendRequest>
    {
        public void Configure(EntityTypeBuilder<FriendRequest> builder)
        {
            builder.HasKey(f => f.FrindRequestId);
            builder.HasOne(f => f.FriendShip) 
             .WithMany()
            .HasForeignKey(f => f.FriendShipId)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
