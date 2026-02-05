using Conexa.TestMovies.Application.Models;
using MediatR;

namespace Conexa.TestMovies.Application.Features.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<BaseResponse>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
