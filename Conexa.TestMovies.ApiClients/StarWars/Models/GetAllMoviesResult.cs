using System;
using System.Collections.Generic;
using System.Text;

namespace Conexa.TestMovies.ApiClients.StarWars.Models
{
    public class GetAllMoviesResult
    {
        public MovieProperties Properties { get; set; }
        public string _id { get; set; }
        public string Description { get; set; }
        public string Uid { get; set; }
        public int __v { get; set; }
    }
}
