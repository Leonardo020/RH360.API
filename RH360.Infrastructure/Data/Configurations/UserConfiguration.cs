using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RH360.Domain.Entities;

namespace RH360.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.PasswordHash)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasDefaultValue(DateTime.UtcNow);

            builder.Property(x => x.UpdatedAt)
                .HasDefaultValue(DateTime.UtcNow);

            builder.Property(x => x.DeletedAt)
                .IsRequired(false);
        }
    }
}
