using Conexa.TestMovies.Application.Common;
using Conexa.TestMovies.Application.Helpers;
using Conexa.TestMovies.Application.Models;
using Conexa.TestMovies.Domain.Interfaces;
using MediatR;
namespace Conexa.TestMovies.Application.Features.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, BaseResponse>
    {
        private IUserRepository _userRepository;
        private ITokenGeneretor _tokenGenerator;
        public LoginUserCommandHandler(IUserRepository userRepository, ITokenGeneretor tokenGeneretor)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGeneretor;
        }
        public async Task<BaseResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return BaseResponse.Failure(null, new List<string> { "Credenciales invalidas." }, 404);

            var token = _tokenGenerator.GenerateToken(user);

            return BaseResponse.Success(token, 200);
        }
    }
}
