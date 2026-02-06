using Conexa.TestMovies.Application.Models;
using Conexa.TestMovies.Domain.Interfaces;
using MediatR;

namespace Conexa.TestMovies.Application.Features.Querys.GetDetailsOfMovie
{
    public class GetDetailsOfMovieQueryHandler : IRequestHandler<GetDetailsOfMovieQuery, BaseResponse>
    {
        private readonly IMovieRepository _movieRepository;
        public GetDetailsOfMovieQueryHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task<BaseResponse> Handle(GetDetailsOfMovieQuery request, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetByIdAsync(request.id);

            if(movie == null) 
                return BaseResponse.Failure(null, new List<string> { "Pelicula no encontrada" }, 404);

            return BaseResponse.Success(movie, 200);
        }
    }
}
