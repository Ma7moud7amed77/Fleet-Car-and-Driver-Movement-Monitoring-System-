using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestGP.Configuration
{

    public class configAss : IEntityTypeConfiguration<Assistant>
    {
        public void Configure(EntityTypeBuilder<Assistant> builder)
        {
            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.Phone).IsUnique();
        }
    }
}
