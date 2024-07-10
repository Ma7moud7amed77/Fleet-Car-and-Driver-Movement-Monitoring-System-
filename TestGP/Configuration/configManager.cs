using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestGP.Models;

namespace TestGP.Configuration
{
    public class configManager : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.Phone).IsUnique();
        }
    }
}
