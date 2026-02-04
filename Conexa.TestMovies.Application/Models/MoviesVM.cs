using Conexa.TestMovies.ApiClients.StarWars.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conexa.TestMovies.Application.Models
{
    public class MoviesVM
    {
        public List<MovieVM> Movies { get; set; }

        public MoviesVM(List<GetAllMoviesResult> movies)
        {
            Movies = movies.Select(m => new MovieVM(m.Properties)).ToList();
        }
    }

}
