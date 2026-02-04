using Conexa.TestMovies.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conexa.TestMovies.Persistence.Configuration
{
    internal static class BaseEntityConfiguration 
    {
        public static void AddBaseEntityFields<T>(this EntityTypeBuilder<T> builder) where T : BaseEntity
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnType("integer")
                .ValueGeneratedOnAdd()
                .IsRequired();
        }
    }
}
