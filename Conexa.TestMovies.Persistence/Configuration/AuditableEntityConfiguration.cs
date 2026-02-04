using Conexa.TestMovies.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conexa.TestMovies.Persistence.Configuration
{
    internal static class AuditableEntityConfiguration
    {
        public static void AddAuditableEntityFields<T>(this EntityTypeBuilder<T> builder) where T : AuditableEntity
        {
            builder.AddBaseEntityFields();

            builder.Property(x => x.LastChangedBy)
                .HasColumnType("TEXT")
                .IsRequired();

            builder.Property(x => x.LastChangedAt)
                .HasColumnType("TEXT")
                .IsRequired();
        }
    }
}
