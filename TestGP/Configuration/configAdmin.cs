using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestGP.Configuration
{
    public class configAdmin : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasIndex(u=>u.Email).IsUnique();
            builder.HasIndex(u=>u.Phone).IsUnique();
        }
    }
}
