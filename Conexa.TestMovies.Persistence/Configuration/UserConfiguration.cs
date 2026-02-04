using Conexa.TestMovies.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Conexa.TestMovies.Persistence.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("TEXT")
                .HasColumnName("username");

            builder.Property(u => u.Email)
                .IsRequired()
                .HasColumnType("TEXT")
                .HasColumnName("email");

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasColumnType("TEXT")
                .HasColumnName("password_hash");

            builder.Property(u => u.IdRole)
                .IsRequired()
                .HasColumnType("INTEGER")
                .HasColumnName("id_role");

            builder.AddAuditableEntityFields();
        }
    }
}
