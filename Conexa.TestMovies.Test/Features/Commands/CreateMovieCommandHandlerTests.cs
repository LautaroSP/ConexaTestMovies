using System.Threading;
using System.Threading.Tasks;
using Conexa.TestMovies.Application.Features.Commands.CreateMovie;
using Conexa.TestMovies.Application.Models;
using Conexa.TestMovies.Domain.Entities;
using Conexa.TestMovies.Domain.Interfaces;
using Moq;
using Shouldly;
using Xunit;

namespace Conexa.TestMovies.Tests.Application.Features.Commands.CreateMovie
{
    public class CreateMovieCommandHandlerTests
    {
        private readonly Mock<IMovieRepository> _movieRepositoryMock;
        private readonly CreateMovieCommandHandler _handler;

        public CreateMovieCommandHandlerTests()
        {
            _movieRepositoryMock = new Mock<IMovieRepository>();
            _handler = new CreateMovieCommandHandler(_movieRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_When_MovieTitleAlreadyExists()
        {
            // Arrange
            var movie = new Movie
            {
                Title = "Star Wars",
                Description = "Space saga",
                ReleaseDate = DateTime.Now
            };

            var command = new CreateMovieCommand { movie = movie };

            _movieRepositoryMock
                .Setup(repo => repo.GetByTitleAsync(movie.Title))
                .ReturnsAsync(movie);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.IsSuccesful.ShouldBeFalse();
            result.Code.ShouldBe(400);
            result.Errors.ShouldContain("Ya existe una pelicula con este titulo");
            result.Result.ShouldBeNull();

            _movieRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Movie>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Should_CreateMovieAndReturnSuccess_When_TitleDoesNotExist()
        {
            // Arrange
            var movie = new Movie
            {
                Title = "Star Wars",
                Description = "Space saga",
                ReleaseDate = DateTime.Now
            };

            var command = new CreateMovieCommand { movie = movie };

            _movieRepositoryMock
                .Setup(repo => repo.GetByTitleAsync(movie.Title))
                .ReturnsAsync((Movie?)null);

            _movieRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Movie>()))
                .ReturnsAsync((Movie m) => m);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.IsSuccesful.ShouldBeTrue();
            result.Code.ShouldBe(201);
            result.Errors.ShouldBeNull();
            result.Result.ShouldNotBeNull();

            var createdMovie = result.Result as Movie;
            createdMovie.ShouldNotBeNull();
            createdMovie.Title.ShouldBe(movie.Title);
            createdMovie.Description.ShouldBe(movie.Description);
            createdMovie.ReleaseDate.ShouldBe(movie.ReleaseDate);

            _movieRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Movie>()), Times.Once);
        }
    }
}
