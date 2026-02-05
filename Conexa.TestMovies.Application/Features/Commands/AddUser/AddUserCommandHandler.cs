using Conexa.TestMovies.Application.Helpers;
using Conexa.TestMovies.Application.Models;
using Conexa.TestMovies.Domain.Entities;
using Conexa.TestMovies.Domain.Interfaces;
using MediatR;
namespace Conexa.TestMovies.Application.Features.Commands.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, BaseResponse>
    {
        private readonly IUserRepository _userRepository;
        public AddUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<BaseResponse> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email!);

            if(user != null)
                return BaseResponse.Failure(null, new List<string> { "El email indicado ya está en uso." }, 400);

            var newUser = new User(request.UserName ,request.Email, HashHelper.HashPassword(request.Password), request.IdRole);

            await _userRepository.AddAsync(newUser);
            return BaseResponse.Success(newUser, 200);
        }


    }
}
