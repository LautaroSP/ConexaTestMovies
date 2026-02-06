using System.Threading;
using System.Threading.Tasks;
using Conexa.TestMovies.Application.Features.Commands.AddUser;
using Conexa.TestMovies.Application.Models;
using Conexa.TestMovies.Domain.Entities;
using Conexa.TestMovies.Domain.Interfaces;
using Moq;
using Shouldly;
using Xunit;

namespace Conexa.TestMovies.Tests.Application.Features.Commands.AddUser
{
    public class AddUserCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly AddUserCommandHandler _handler;

        public AddUserCommandHandlerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _handler = new AddUserCommandHandler(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_When_EmailAlreadyExists()
        {
            // Arrange
            var command = new AddUserCommand
            {
                UserName = "lautaro",
                Email = "lautaro@test.com",
                Password = "123456",
                IdRole = 1
            };

            _userRepositoryMock
                .Setup(repo => repo.GetByEmailAsync(command.Email))
                .ReturnsAsync(new User("existing", command.Email, "hash", 1));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.IsSuccesful.ShouldBeFalse();
            result.Code.ShouldBe(400);
            result.Errors.ShouldContain("El email indicado ya está en uso.");
            result.Result.ShouldBeNull();

            _userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Should_CreateUserAndReturnSuccess_When_EmailDoesNotExist()
        {
            // Arrange
            var command = new AddUserCommand
            {
                UserName = "lautaro",
                Email = "lautaro@test.com",
                Password = "123456",
                IdRole = 1
            };

            _userRepositoryMock
                .Setup(repo => repo.GetByEmailAsync(command.Email))
                .ReturnsAsync((User?)null);

            _userRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<User>()))
                .ReturnsAsync((User u) => u);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.IsSuccesful.ShouldBeTrue();
            result.Code.ShouldBe(201);
            result.Errors.ShouldBeNull();
            result.Result.ShouldBe(null);

            _userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>()), Times.Once);
        }

    }
}
