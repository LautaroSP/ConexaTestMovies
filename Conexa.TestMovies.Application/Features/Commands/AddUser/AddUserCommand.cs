using Conexa.TestMovies.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Conexa.TestMovies.Application.Features.Commands.AddUser
{

    public class AddUserCommand : IRequest<BaseResponse>
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int IdRole { get; set; }
    }
}
