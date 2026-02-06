using Conexa.TestMovies.Application.Models;
using MediatR;

namespace Conexa.TestMovies.Application.Features.Querys.GetDetailsOfMovie
{
    public class GetDetailsOfMovieQuery : IRequest<BaseResponse>
    {
        public int id { get; set; }
    }
}
