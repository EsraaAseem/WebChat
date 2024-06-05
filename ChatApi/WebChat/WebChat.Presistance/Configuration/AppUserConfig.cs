using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebChat.Domain.Entities;

namespace WebChat.Presistance.Configuration
{
    public class AppUserConfig:IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(f => f.Id);
        }
    }
}
