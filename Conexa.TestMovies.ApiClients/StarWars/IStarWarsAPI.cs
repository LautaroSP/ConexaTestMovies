using Conexa.TestMovies.ApiClients.StarWars.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conexa.TestMovies.ApiClients.StarWars
{
    public interface IStarWarsAPI
    {
        [Get("/api/films/")]
        Task<IApiResponse<GetAllMoviesResponse>> GetAllMovies();

    }
}
