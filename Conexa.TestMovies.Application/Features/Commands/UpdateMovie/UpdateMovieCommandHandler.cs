using Conexa.TestMovies.Application.Models;
using Conexa.TestMovies.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conexa.TestMovies.Application.Features.Commands.UpdateMovie
{
    public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, BaseResponse>
    {
        private readonly IMovieRepository _movieRepository;
        public UpdateMovieCommandHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task<BaseResponse> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetByIdAsync(request.id);
            if (movie == null)
                return BaseResponse.Failure(false, new List<string> { "No se encontro la pelicula" }, 404);

            movie.Title = request.movie.Title;
            movie.Description = request.movie.Description;
            movie.Director = request.movie.Director;
            movie.ReleaseDate = request.movie.ReleaseDate;
            await _movieRepository.UpdateAsync(movie);
            return BaseResponse.Success(true, 200);
        }
    }
}
