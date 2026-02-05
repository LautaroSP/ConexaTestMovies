using Conexa.TestMovies.Domain.Shared;
using System.Runtime.CompilerServices;

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

        public User(string? username, string? email, string? passwordHash, int idRole)
        {
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            IdRole = idRole;    
        }
    }
}
