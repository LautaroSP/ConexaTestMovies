using Conexa.TestMovies.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conexa.TestMovies.Application.Features.Querys.GetAllMovies
{
    public class GetAllMoviesQuery : IRequest<BaseResponse>
    {
    }
}
