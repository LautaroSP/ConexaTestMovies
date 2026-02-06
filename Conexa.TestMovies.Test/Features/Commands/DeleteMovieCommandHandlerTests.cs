using System.Threading;
using System.Threading.Tasks;
using Conexa.TestMovies.Application.Features.Commands.DeleteMovie;
using Conexa.TestMovies.Application.Models;
using Conexa.TestMovies.Domain.Entities;
using Conexa.TestMovies.Domain.Interfaces;
using Moq;
using Shouldly;
using Xunit;

namespace Conexa.TestMovies.Tests.Application.Features.Commands.DeleteMovie
{
    public class DeleteMovieCommandHandlerTests
    {
        private readonly Mock<IMovieRepository> _movieRepositoryMock;
        private readonly DeleteMovieCommandHandler _handler;

        public DeleteMovieCommandHandlerTests()
        {
            _movieRepositoryMock = new Mock<IMovieRepository>();
            _handler = new DeleteMovieCommandHandler(_movieRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_When_MovieDoesNotExist()
        {
            // Arrange
            var command = new DeleteMovieCommand { Id = 1 };

            _movieRepositoryMock
                .Setup(repo => repo.GetByIdAsync(command.Id))
                .ReturnsAsync((Movie?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.IsSuccesful.ShouldBeFalse();
            result.Code.ShouldBe(404);
            result.Errors.ShouldContain("No se encontro la pelicula");
            result.IsSuccesful.ShouldBeFalse(); 
            result.Result.ShouldBeNull();
            _movieRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Movie>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Should_DeleteMovieAndReturnSuccess_When_MovieExists()
        {
            // Arrange
            var movie = new Movie
            {
                Id = 1,
                Title = "Star Wars"
            };

            var command = new DeleteMovieCommand { Id = movie.Id };

            _movieRepositoryMock
                .Setup(repo => repo.GetByIdAsync(command.Id))
                .ReturnsAsync(movie);

            _movieRepositoryMock
                .Setup(repo => repo.DeleteAsync(movie))
                .ReturnsAsync(movie);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.IsSuccesful.ShouldBeTrue();
            result.Code.ShouldBe(200);
            result.Errors.ShouldBeNull();
            result.Result.ShouldBe(200); 

            _movieRepositoryMock.Verify(repo => repo.DeleteAsync(movie), Times.Once);
        }
    }
}
