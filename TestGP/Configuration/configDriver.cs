using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestGP.Models;
namespace TestGP.Configuration
{
       public class configDriver : IEntityTypeConfiguration<Driver>
        {
            public void Configure(EntityTypeBuilder<Driver> builder)
            {
                builder.HasIndex(u => u.Email).IsUnique();
                builder.HasIndex(u => u.Phone).IsUnique();
            }
        }
    }

