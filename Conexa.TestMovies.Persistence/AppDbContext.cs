using Conexa.TestMovies.Domain.Entities;
using Conexa.TestMovies.Domain.Shared;
using Conexa.TestMovies.Persistence.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Conexa.TestMovies.Persistence
{
    public class AppDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        { 
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("ConexaMovies");
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new MovieConfiguration());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach(var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if(entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    entry.Entity.LastChangedAt = DateTime.Now;
                    entry.Entity.LastChangedBy = GetUserByToken(); 
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        private string GetUserByToken()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserName");
            return userIdClaim != null ? userIdClaim.Value : "Unknown";
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}
