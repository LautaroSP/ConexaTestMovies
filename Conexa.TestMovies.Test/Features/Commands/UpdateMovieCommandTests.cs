using Conexa.TestMovies.Application.Features.Commands.UpdateMovie;
using Conexa.TestMovies.Application.Models;
using Conexa.TestMovies.Domain.Entities;
using Conexa.TestMovies.Domain.Interfaces;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Conexa.TestMovies.Application.Tests.Features.Commands.UpdateMovie
{
    public class UpdateMovieCommandHandlerTests
    {
        private readonly Mock<IMovieRepository> _movieRepositoryMock;
        private readonly UpdateMovieCommandHandler _handler;

        public UpdateMovieCommandHandlerTests()
        {
            _movieRepositoryMock = new Mock<IMovieRepository>();
            _handler = new UpdateMovieCommandHandler(_movieRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_UpdateMovieAndReturnSuccess_When_MovieExists()
        {
            // Arrange
            var existingMovie = new Movie
            {
                Id = 1,
                Title = "Old Title",
                Description = "Old Description",
                Director = "Old Director",
                ReleaseDate = new DateTime(2000, 1, 1)
            };

            var updatedMovie = new Movie
            {
                Title = "New Title",
                Description = "New Description",
                Director = "New Director",
                ReleaseDate = new DateTime(2024, 1, 1)
            };

            var command = new UpdateMovieCommand
            {
                id = existingMovie.Id,
                movie = updatedMovie
            };

            _movieRepositoryMock
                .Setup(repo => repo.GetByIdAsync(command.id))
                .ReturnsAsync(existingMovie);

            _movieRepositoryMock
                .Setup(repo => repo.UpdateAsync(existingMovie))
                .ReturnsAsync(existingMovie);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            result.ShouldNotBeNull();
            result.IsSuccesful.ShouldBeTrue();
            result.Code.ShouldBe(200);
            result.Errors.ShouldBeNull();
            result.Result.ShouldBe(true);

            existingMovie.Title.ShouldBe(updatedMovie.Title);
            existingMovie.Description.ShouldBe(updatedMovie.Description);
            existingMovie.Director.ShouldBe(updatedMovie.Director);
            existingMovie.ReleaseDate.ShouldBe(updatedMovie.ReleaseDate);

            _movieRepositoryMock.Verify(repo => repo.UpdateAsync(existingMovie), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_When_MovieDoesNotExist()
        {
            // Arrange
            var command = new UpdateMovieCommand
            {
                id = 99,
                movie = new Movie()
            };

            _movieRepositoryMock
                .Setup(repo => repo.GetByIdAsync(command.id))
                .ReturnsAsync((Movie?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.IsSuccesful.ShouldBeFalse();
            result.Code.ShouldBe(404);
            result.Errors.ShouldContain("No se encontro la pelicula");

            _movieRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Movie>()), Times.Never);
        }
    }
}
