using Conexa.TestMovies.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conexa.TestMovies.Persistence.Configuration
{
    internal class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("movies");
            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("TEXT")
                .HasColumnName("title");

            builder.Property(m => m.Description)
                .HasColumnType("TEXT")
                .HasColumnName("description");

            builder.Property(m => m.ReleaseDate)
                .HasColumnType("TEXT")
                .HasColumnName("release_date");

            builder.Property(m => m.Director)
                .HasMaxLength(100)
                .HasColumnType("TEXT")
                .HasColumnName("director");
        }
    }
}
