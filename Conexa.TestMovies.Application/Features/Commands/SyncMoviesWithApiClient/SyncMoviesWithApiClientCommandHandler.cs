using Conexa.TestMovies.ApiClients.StarWars;
using Conexa.TestMovies.Application.Features.Querys.GetAllMoviesStarWars;
using Conexa.TestMovies.Application.Models;
using Conexa.TestMovies.Domain.Entities;
using Conexa.TestMovies.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Conexa.TestMovies.Application.Features.Commands.SyncMoviesWithApiClient
{
    public class SyncMoviesWithApiClientCommandHandler : IRequestHandler<SyncMoviesWithApiClientCommand, BaseResponse>
    {
        private readonly IStarWarsAPI _starWarsAPI;
        private readonly IMovieRepository _movieRepository;

        public SyncMoviesWithApiClientCommandHandler(IStarWarsAPI starWarsAPI, IMovieRepository movieRepository)
        {
            _starWarsAPI = starWarsAPI;
            _movieRepository = movieRepository;
        }
        public async Task<BaseResponse> Handle(SyncMoviesWithApiClientCommand request, CancellationToken cancellationToken)
        {
            var response = await _starWarsAPI.GetAllMovies();
            var moviesAdded = new List<Movie>();
            if (response == null || !response.IsSuccessStatusCode || response.Content == null)
            {
                return BaseResponse.Failure(null, code: (int)HttpStatusCode.ServiceUnavailable, errors: new List<string>
                {
                    "El servicio no se encuentra disponible"
                });
            }

            var result = new MoviesVM(response.Content.Result);

            foreach(var movie in result.Movies)
            {
                var existingMovie = await _movieRepository.GetByTitleAsync(movie.Title);

                if (existingMovie == null)
                {
                    var newMovie = new Movie
                    {
                        Title = movie.Title,
                        Description = movie.Description,
                        ReleaseDate = movie.ReleaseDate,
                        Director = movie.Director
                    };
                    await _movieRepository.AddAsync(newMovie);
                    moviesAdded.Add(newMovie);
                }
            }
            return BaseResponse.Success(moviesAdded, 200);
        }
    }
}
