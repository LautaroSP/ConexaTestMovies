using Conexa.TestMovies.Application.Features.Querys.GetAllMovies;
using Conexa.TestMovies.Application.Models;
using Conexa.TestMovies.Domain.Entities;
using Conexa.TestMovies.Domain.Interfaces;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Conexa.TestMovies.Application.Tests.Features.Querys.GetAllMovies
{
    public class GetAllMoviesQueryHandlerTests
    {
        private readonly Mock<IMovieRepository> _movieRepositoryMock;
        private readonly GetAllMoviesQueryHandler _handler;

        public GetAllMoviesQueryHandlerTests()
        {
            _movieRepositoryMock = new Mock<IMovieRepository>();
            _handler = new GetAllMoviesQueryHandler(_movieRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnAllMovies_When_RepositoryReturnsData()
        {
            // Arrange
            var movies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Movie 1" },
                new Movie { Id = 2, Title = "Movie 2" }
            };

            _movieRepositoryMock
                .Setup(repo => repo.ListAllAsync())
                .ReturnsAsync(movies);

            var query = new GetAllMoviesQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.IsSuccesful.ShouldBeTrue();
            result.Code.ShouldBe(200);
            result.Errors.ShouldBeNull();

            var returnedMovies = result.Result as List<Movie>;
            returnedMovies.ShouldNotBeNull();
            returnedMovies.Count.ShouldBe(2);
            returnedMovies.ShouldBe(movies);

            _movieRepositoryMock.Verify(repo => repo.ListAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnEmptyList_When_NoMoviesExist()
        {
            // Arrange
            _movieRepositoryMock
                .Setup(repo => repo.ListAllAsync())
                .ReturnsAsync(new List<Movie>());

            var query = new GetAllMoviesQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.IsSuccesful.ShouldBeTrue();
            result.Code.ShouldBe(200);
            result.Errors.ShouldBeNull();

            var returnedMovies = result.Result as List<Movie>;
            returnedMovies.ShouldNotBeNull();
            returnedMovies.Count.ShouldBe(0);

            _movieRepositoryMock.Verify(repo => repo.ListAllAsync(), Times.Once);
        }
    }
}
