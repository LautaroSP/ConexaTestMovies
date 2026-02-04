using Conexa.TestMovies.Domain.Shared;

namespace Conexa.TestMovies.Domain.Entities
{
    public enum UserRole
    {
        Admin = 1,
        User = 2
    }
    public class User : AuditableEntity
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public int IdRole { get; set; }
    }
}
