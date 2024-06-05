using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebChat.Domain.Entities;

namespace WebChat.Persistence.Configuration
{
    public class GroupMessageConfig : IEntityTypeConfiguration<GroupMessages>
    {
            public void Configure(EntityTypeBuilder<GroupMessages> builder)
            {
               
               builder.HasOne(f => f.User)
                .WithMany()
               .HasForeignKey(f => f.SenderId)
              .OnDelete(DeleteBehavior.Restrict);

              
            }
    }
}
