using Conexa.TestMovies.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conexa.TestMovies.Domain.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
    }
}
