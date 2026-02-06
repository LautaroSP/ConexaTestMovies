using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Conexa.TestMovies.ApiClients.StarWars;
using Conexa.TestMovies.ApiClients.StarWars.Models;
using Conexa.TestMovies.Application.Features.Commands.SyncMoviesWithApiClient;
using Conexa.TestMovies.Application.Models;
using Conexa.TestMovies.Domain.Entities;
using Conexa.TestMovies.Domain.Interfaces;
using Moq;
using Refit;
using Shouldly;
using Xunit;

namespace Conexa.TestMovies.Tests.Application.Features.Commands.SyncMoviesWithApiClient
{
    public class SyncMoviesWithApiClientCommandHandlerTests
    {
        private readonly Mock<IStarWarsAPI> _starWarsApiMock;
        private readonly Mock<IMovieRepository> _movieRepositoryMock;
        private readonly SyncMoviesWithApiClientCommandHandler _handler;

        public SyncMoviesWithApiClientCommandHandlerTests()
        {
            _starWarsApiMock = new Mock<IStarWarsAPI>();
            _movieRepositoryMock = new Mock<IMovieRepository>();
            _handler = new SyncMoviesWithApiClientCommandHandler(
                _starWarsApiMock.Object,
                _movieRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_AddOnlyNewMovies_AndReturnThem()
        {
            // Arrange
            var apiResponseContent = new GetAllMoviesResponse
            {
                Message = "ok",
                Result = new List<GetAllMoviesResult>
        {
            new GetAllMoviesResult
            {
                Properties = new MovieProperties
                {
                    Title = "Star Wars",
                    Director = "George Lucas",
                    Release_Date = new DateTime(1977, 5, 25),
                    Opening_Crawl = "A long time ago..."
                }
            },
            new GetAllMoviesResult
            {
                Properties = new MovieProperties
                {
                    Title = "The Empire Strikes Back",
                    Director = "Irvin Kershner",
                    Release_Date = new DateTime(1980, 5, 21),
                    Opening_Crawl = "It is a dark time for the Rebellion..."
                }
            }
        }
            };

            var apiResponseMock = new Mock<IApiResponse<GetAllMoviesResponse>>();
            apiResponseMock.Setup(r => r.IsSuccessStatusCode).Returns(true);
            apiResponseMock.Setup(r => r.Content).Returns(apiResponseContent);

            _starWarsApiMock
                .Setup(api => api.GetAllMovies())
                .ReturnsAsync(apiResponseMock.Object);

            _movieRepositoryMock
                .Setup(repo => repo.GetByTitleAsync("Star Wars"))
                .ReturnsAsync(new Movie { Title = "Star Wars" });

            _movieRepositoryMock
                .Setup(repo => repo.GetByTitleAsync("The Empire Strikes Back"))
                .ReturnsAsync((Movie?)null);

            _movieRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Movie>()))
                .ReturnsAsync((Movie m) => m);

            // Act
            var result = await _handler.Handle(new SyncMoviesWithApiClientCommand(), CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.IsSuccesful.ShouldBeTrue();
            result.Code.ShouldBe(200);
            result.Errors.ShouldBeNull();

            var addedMovies = result.Result as List<Movie>;
            addedMovies.ShouldNotBeNull();
            addedMovies.Count.ShouldBe(1);
            addedMovies[0].Title.ShouldBe("The Empire Strikes Back");

            _movieRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Movie>()), Times.Once);
        }

    }
}
