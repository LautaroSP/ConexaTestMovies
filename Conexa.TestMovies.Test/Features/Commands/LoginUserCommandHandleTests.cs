using Conexa.TestMovies.Application.Common;
using Conexa.TestMovies.Application.Features.Commands.LoginUser;
using Conexa.TestMovies.Application.Models;
using Conexa.TestMovies.Domain.Entities;
using Conexa.TestMovies.Domain.Interfaces;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Conexa.TestMovies.Tests.Application.Features.Commands.LoginUser
{
    public class LoginUserCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ITokenGeneretor> _tokenGeneratorMock;
        private readonly LoginUserCommandHandler _handler;

        public LoginUserCommandHandlerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _tokenGeneratorMock = new Mock<ITokenGeneretor>();
            _handler = new LoginUserCommandHandler(_userRepositoryMock.Object, _tokenGeneratorMock.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_When_UserDoesNotExist()
        {
            // Arrange
            var command = new LoginUserCommand
            {
                Email = "lautaro@test.com",
                Password = "123456"
            };

            _userRepositoryMock
                .Setup(repo => repo.GetByEmailAsync(command.Email))
                .ReturnsAsync((User?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.IsSuccesful.ShouldBeFalse();
            result.Code.ShouldBe(404);
            result.Errors.ShouldContain("Credenciales invalidas.");
            result.Result.ShouldBeNull();

            _tokenGeneratorMock.Verify(gen => gen.GenerateToken(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_When_PasswordIsIncorrect()
        {
            // Arrange
            var password = "123456";
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword("differentPassword");

            var user = new User("lautaro", "lautaro@test.com", hashedPassword, 1);

            var command = new LoginUserCommand
            {
                Email = user.Email!,
                Password = password
            };

            _userRepositoryMock
                .Setup(repo => repo.GetByEmailAsync(command.Email))
                .ReturnsAsync(user);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.IsSuccesful.ShouldBeFalse();
            result.Code.ShouldBe(404);
            result.Errors.ShouldContain("Credenciales invalidas.");
            result.Result.ShouldBeNull();

            _tokenGeneratorMock.Verify(gen => gen.GenerateToken(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessAndToken_When_CredentialsAreValid()
        {
            // Arrange
            var password = "123456";
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User("lautaro", "lautaro@test.com", hashedPassword, 1);

            var command = new LoginUserCommand
            {
                Email = user.Email!,
                Password = password
            };

            var expectedToken = "fake-jwt-token";

            _userRepositoryMock
                .Setup(repo => repo.GetByEmailAsync(command.Email))
                .ReturnsAsync(user);

            _tokenGeneratorMock
                .Setup(gen => gen.GenerateToken(user))
                .Returns(expectedToken);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.IsSuccesful.ShouldBeTrue();
            result.Code.ShouldBe(200);
            result.Errors.ShouldBeNull();
            result.Result.ShouldBe(expectedToken);

            _tokenGeneratorMock.Verify(gen => gen.GenerateToken(user), Times.Once);
        }
    }
}
