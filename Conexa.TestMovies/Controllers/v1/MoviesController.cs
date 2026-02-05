using Conexa.TestMovies.ApiClients.StarWars;
using Conexa.TestMovies.ApiClients.StarWars.Models;
using Conexa.TestMovies.Application.Features.Commands.DeleteMovie;
using Conexa.TestMovies.Application.Features.Commands.SyncMoviesWithApiClient;
using Conexa.TestMovies.Application.Features.Commands.UpdateMovie;
using Conexa.TestMovies.Application.Features.Querys.GetAllMovies;
using Conexa.TestMovies.Application.Features.Querys.GetAllMoviesStarWars;
using Conexa.TestMovies.Application.Models;
using Conexa.TestMovies.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;

namespace Conexa.TestMovies.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MoviesController : BaseController
    {
        private readonly IMediator _mediator;
        public MoviesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("/api/starwars")]
        [ProducesResponseType(typeof(MoviesVM), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [EndpointSummary("Obtengo la lista de peliculas de la API https://www.swapi.tech/ ")]
        public async Task<IActionResult> GetStarWarsMovies()
        {
            var result = await _mediator.Send(new GetAllMoviesStarWarsQuery());
            return ProcessResult(result);
        }


        [Authorize]
        [HttpPost("/api/syncMovies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [EndpointSummary("Sincronizacion de datos de la bdd con la API: https://www.swapi.tech/ ")]
        public async Task<IActionResult> SyncMoviesWithApiClient()
        {
            var result = await _mediator.Send(new SyncMoviesWithApiClientCommand());
            return ProcessResult(result);
        }


        [Authorize]
        [HttpPost("/api/delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [EndpointSummary("Se elimina una pelicula por su Id")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var result = await _mediator.Send(new DeleteMovieCommand { Id = id });
            return ProcessResult(result);
        }

        [HttpGet("/api/movies")]
        [ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [EndpointSummary("Se obtienen todas las peliculas")]
        public async Task<IActionResult> GetMovies()
        {
            var result = await _mediator.Send(new GetAllMoviesQuery());
            return ProcessResult(result);
        }

        [Authorize]
        [HttpPut("api/movies/update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [EndpointSummary("Se actualiza una pelicula por su id")]
        public async Task<IActionResult> UpdateMovie([FromBody]Movie movie , int id)
        {
            var result = await _mediator.Send(new UpdateMovieCommand { movie = movie,id = id});
            return ProcessResult(result);
        }
    }
}
