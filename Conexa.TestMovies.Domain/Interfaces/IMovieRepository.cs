using Conexa.TestMovies.Domain.Entities;

namespace Conexa.TestMovies.Domain.Interfaces
{
    public interface IMovieRepository : IAsyncRepository<Movie>
    { 
    }
}
