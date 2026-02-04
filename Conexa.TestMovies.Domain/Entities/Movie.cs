using Conexa.TestMovies.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conexa.TestMovies.Domain.Entities
{
    public class Movie : AuditableEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Director { get; set; }
    }
}
