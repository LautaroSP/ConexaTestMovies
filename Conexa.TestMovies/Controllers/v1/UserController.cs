using Conexa.TestMovies.Application.Features.Commands.AddUser;
using Conexa.TestMovies.Application.Features.Commands.LoginUser;
using Conexa.TestMovies.Application.Models;
using Conexa.TestMovies.Controllers.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Conexa.TestMovies.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(UserVM),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [EndpointSummary("Registra un nuevo usuario")]
        public async Task<IActionResult> RegisterUser([FromBody] AddUserRequest request)
        {
            var result = await _mediator.Send(new AddUserCommand
            {
                UserName = request.UserName,
                Email = request.Email,
                Password = request.Password,
                IdRole = request.IdRole
            });

            return ProcessResult(result);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(UserVM), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [EndpointSummary("Login del usuario")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserRequest request)
        {
            var result = await _mediator.Send(new LoginUserCommand
            {
                Email = request.Email,
                Password = request.Password,
            });

            return ProcessResult(result);
        }
    }
}
