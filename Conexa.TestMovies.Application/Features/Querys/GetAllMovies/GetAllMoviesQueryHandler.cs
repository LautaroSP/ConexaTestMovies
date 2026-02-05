using Conexa.TestMovies.Application.Models;
using Conexa.TestMovies.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conexa.TestMovies.Application.Features.Querys.GetAllMovies
{
    internal class GetAllMoviesQueryHandler : IRequestHandler<GetAllMoviesQuery, BaseResponse>
    {
        private readonly IMovieRepository _moviesRepository;
        public GetAllMoviesQueryHandler(IMovieRepository movieRepository)
        {
            _moviesRepository = movieRepository;
        }
        public async Task<BaseResponse> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
        {
            var result = await _moviesRepository.ListAllAsync();

            return BaseResponse.Success(result);
        }
    }
}
