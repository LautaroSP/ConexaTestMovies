using Conexa.TestMovies.Application.Models;
using MediatR;

namespace Conexa.TestMovies.Application.Features.Commands.DeleteMovie
{
    public class DeleteMovieCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }
}
