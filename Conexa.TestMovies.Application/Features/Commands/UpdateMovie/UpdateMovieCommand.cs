using Conexa.TestMovies.Application.Models;
using Conexa.TestMovies.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conexa.TestMovies.Application.Features.Commands.UpdateMovie
{
    public class UpdateMovieCommand : IRequest<BaseResponse>
    {
        public Movie movie { get; set; }
        public int id { get; set; }
    }
}
