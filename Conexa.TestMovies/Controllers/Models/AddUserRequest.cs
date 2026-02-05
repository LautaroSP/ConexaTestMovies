using System.ComponentModel.DataAnnotations;

namespace Conexa.TestMovies.Controllers.Models
{
    public class AddUserRequest
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public int IdRole { get; set; }
    }
}
