using Conexa.TestMovies.Application.Features.Querys.GetDetailsOfMovie;
using Conexa.TestMovies.Application.Models;
using Conexa.TestMovies.Domain.Entities;
using Conexa.TestMovies.Domain.Interfaces;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Conexa.TestMovies.Application.Tests.Features.Querys.GetDetailsOfMovie
{
    public class GetDetailsOfMovieQueryHandlerTests
    {
        private readonly Mock<IMovieRepository> _movieRepositoryMock;
        private readonly GetDetailsOfMovieQueryHandler _handler;

        public GetDetailsOfMovieQueryHandlerTests()
        {
            _movieRepositoryMock = new Mock<IMovieRepository>();
            _handler = new GetDetailsOfMovieQueryHandler(_movieRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnMovie_When_MovieExists()
        {
            // Arrange
            var movie = new Movie
            {
                Id = 1,
                Title = "Star Wars",
                Description = "Sci-Fi",
                Director = "George Lucas"
            };

            var query = new GetDetailsOfMovieQuery { id = movie.Id };

            _movieRepositoryMock
                .Setup(repo => repo.GetByIdAsync(query.id))
                .ReturnsAsync(movie);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.IsSuccesful.ShouldBeTrue();
            result.Code.ShouldBe(200);
            result.Errors.ShouldBeNull();

            var returnedMovie = result.Result as Movie;
            returnedMovie.ShouldNotBeNull();
            returnedMovie.ShouldBe(movie);

            _movieRepositoryMock.Verify(repo => repo.GetByIdAsync(query.id), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_When_MovieDoesNotExist()
        {
            // Arrange
            var query = new GetDetailsOfMovieQuery { id = 999 };

            _movieRepositoryMock
                .Setup(repo => repo.GetByIdAsync(query.id))
                .ReturnsAsync((Movie?)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.IsSuccesful.ShouldBeFalse();
            result.Code.ShouldBe(404);
            result.Errors.ShouldContain("Pelicula no encontrada");

            _movieRepositoryMock.Verify(repo => repo.GetByIdAsync(query.id), Times.Once);
        }
    }
}
