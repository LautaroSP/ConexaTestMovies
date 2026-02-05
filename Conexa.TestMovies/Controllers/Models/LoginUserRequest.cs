using System.ComponentModel.DataAnnotations;

namespace Conexa.TestMovies.Controllers.Models
{
    public class LoginUserRequest
    {
        [EmailAddress]
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
