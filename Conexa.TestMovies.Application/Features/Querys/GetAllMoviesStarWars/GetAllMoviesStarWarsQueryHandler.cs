using Conexa.TestMovies.ApiClients.StarWars;
using Conexa.TestMovies.ApiClients.StarWars.Models;
using Conexa.TestMovies.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Conexa.TestMovies.Application.Features.Querys.GetAllMoviesStarWars
{
    public class GetAllMoviesStarWarsQueryHandler : IRequestHandler<GetAllMoviesStarWarsQuery, BaseResponse>
    {
        private readonly IStarWarsAPI _starWarsAPI;

        public GetAllMoviesStarWarsQueryHandler(IStarWarsAPI starWarsAPI)
        {
            _starWarsAPI = starWarsAPI;
        }
        public async Task<BaseResponse> Handle(GetAllMoviesStarWarsQuery request, CancellationToken cancellationToken)
        {
            var response = await _starWarsAPI.GetAllMovies();

            if (response == null || !response.IsSuccessStatusCode || response.Content == null)
            {
                return BaseResponse.Failure(null, code: (int)HttpStatusCode.ServiceUnavailable, errors: new List<string>
        {
            "El servicio no se encuentra disponible"
        });
            }

            var result = new MoviesVM(response.Content.Result);

            return BaseResponse.Success(result);
        }

    }
}
