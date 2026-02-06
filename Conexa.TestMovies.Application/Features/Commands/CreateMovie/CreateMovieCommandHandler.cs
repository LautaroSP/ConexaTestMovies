using Conexa.TestMovies.Application.Models;
using Conexa.TestMovies.Domain.Interfaces;
using MediatR;

namespace Conexa.TestMovies.Application.Features.Commands.CreateMovie
{
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, BaseResponse>
    {
        private readonly IMovieRepository _movieRepository;
        public CreateMovieCommandHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task<BaseResponse> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetByTitleAsync(request.movie.Title!);

            if (movie != null)
                return BaseResponse.Failure(null, new List<string> { "Ya existe una pelicula con este titulo" }, 400);
            
            var createdMovie = await _movieRepository.AddAsync(request.movie);
            return BaseResponse.Success(createdMovie, 201);
        }
    }
}
