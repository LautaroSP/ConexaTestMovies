using Conexa.TestMovies.Domain.Entities;
using Conexa.TestMovies.Domain.Interfaces;
using Conexa.TestMovies.Persistence.Repository.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conexa.TestMovies.Persistence.Repository
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        public async Task<Movie> GetByTitleAsync(string title)
        {
            return await _ctx.Movies.FirstOrDefaultAsync(m => m.Title == title) ?? null!;
        }
    }
}