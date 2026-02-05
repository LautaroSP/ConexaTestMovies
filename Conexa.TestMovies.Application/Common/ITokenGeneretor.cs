using Conexa.TestMovies.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conexa.TestMovies.Application.Common
{
    public interface ITokenGeneretor
    {
        string GenerateToken(User user);
    }
}
