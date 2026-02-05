using System;
using System.Collections.Generic;
using System.Text;

namespace Conexa.TestMovies.Application.Helpers
{
    public static class HashHelper
    {
        public static string HashPassword(string? password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
