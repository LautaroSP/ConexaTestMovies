using Conexa.TestMovies.Domain.Entities;
using Conexa.TestMovies.Domain.Interfaces;
using Conexa.TestMovies.Persistence.Repository.Shared;
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
    }
}