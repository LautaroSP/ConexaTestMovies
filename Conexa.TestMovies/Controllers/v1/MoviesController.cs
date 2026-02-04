using Conexa.TestMovies.ApiClients.StarWars;
using Conexa.TestMovies.ApiClients.StarWars.Models;
using Conexa.TestMovies.Application.Features.Querys.GetAllMoviesStarWars;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace Conexa.TestMovies.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MoviesController : Controller
    {
        private readonly IMediator _mediator;
        public MoviesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("/api/starwars")]
        public async Task<IActionResult> GetStarWarsMovies()
        {
            var result = await _mediator.Send(new GetAllMoviesStarWarsQuery());
            return Ok(result);
        }
    }
}
