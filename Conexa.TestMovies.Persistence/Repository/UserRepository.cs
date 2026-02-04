using Conexa.TestMovies.Domain.Entities;
using Conexa.TestMovies.Persistence.Repository.Shared;
using Conexa.TestMovies.Domain.Interfaces;

namespace Conexa.TestMovies.Persistence.Repository
{
    internal class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
