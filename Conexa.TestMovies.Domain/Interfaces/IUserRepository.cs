using Conexa.TestMovies.Domain.Entities;

namespace Conexa.TestMovies.Domain.Interfaces
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<User> GetByEmailAsync(string email);

    }
}
