using Conexa.TestMovies.Domain.Interfaces;
using Conexa.TestMovies.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Conexa.TestMovies.Persistence.Repository.Shared
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _ctx;

        public BaseRepository(AppDbContext appDbContext)
        {
            _ctx = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));  
        }
        public async Task<T> AddAsync(T entity)
        {
            _ctx.Set<T>().Add(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }
        public async Task<T> DeleteAsync(T entity)
        {
            _ctx.Set<T>().Remove(entity);
            await _ctx.SaveChangesAsync();
            return entity;  
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _ctx.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _ctx.Set<T>().ToListAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _ctx.Entry(entity).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
            return entity;
        }
    }
}
