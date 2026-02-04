using Conexa.TestMovies.ApiClients.StarWars.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conexa.TestMovies.Application.Models
{
    public class MovieVM
    {
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Director { get; set; }
        public string Description { get; set; }
        public MovieVM(MovieProperties properties)
        {
            Title = properties.Title;
            ReleaseDate = properties.Release_Date;
            Director = properties.Director;
            Description = properties.Opening_Crawl;
        }
    }

    
}
