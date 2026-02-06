using Conexa.TestMovies.Application.Models;
using Conexa.TestMovies.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conexa.TestMovies.Application.Features.Commands.DeleteMovie
{
    public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand, BaseResponse>
    {
        private readonly IMovieRepository _movieRepository;
        public DeleteMovieCommandHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task<BaseResponse> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetByIdAsync(request.Id);
            if (movie == null)
                return BaseResponse.Failure(null,new List<string> { "No se encontro la pelicula" }, 404);

            await _movieRepository.DeleteAsync(movie);

            return BaseResponse.Success(200);
        }
    }
}
