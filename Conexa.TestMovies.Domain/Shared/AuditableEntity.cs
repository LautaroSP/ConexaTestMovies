
namespace Conexa.TestMovies.Domain.Shared
{
    public class AuditableEntity : BaseEntity
    {
        public string? LastChangedBy { get; set; }
        public DateTime LastChangedAt { get; set; }

    }
}
