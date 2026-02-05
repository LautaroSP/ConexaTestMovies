using Conexa.TestMovies.Domain.Entities;
using Conexa.TestMovies.Persistence.Repository.Shared;
using Conexa.TestMovies.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Conexa.TestMovies.Persistence.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email) ?? null!;
        }
    }
}
