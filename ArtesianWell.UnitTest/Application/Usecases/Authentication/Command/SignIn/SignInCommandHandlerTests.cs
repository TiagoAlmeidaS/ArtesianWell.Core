using Application.Services.Authentication;
using Application.Services.Authentication.Dtos;
using Application.Usecases.Authentication.Command.SignIn;
using Moq;
using Shared.Messages;

namespace ArtesianWell.UnitTest.Application.Usecases.Authentication.Command.SignIn;

public class SignInCommandHandlerTests
{
    private readonly Mock<IAuthenticationService> _authenticationServiceMock;
    private readonly Mock<IMessageHandlerService> _messageHandlerServiceMock;
    private readonly SignInCommandHandler _handler;

    public SignInCommandHandlerTests()
    {
        _authenticationServiceMock = new Mock<IAuthenticationService>();
        _messageHandlerServiceMock = new Mock<IMessageHandlerService>();
        _handler = new SignInCommandHandler(_authenticationServiceMock.Object, _messageHandlerServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSignInResult_WhenSignInIsSuccessful()
    {
        // Arrange
        var signInCommand = new SignInCommand
        {
            Code = "testCode",
            Password = "testPassword",
            Key = "testKey"
        };
        
        var tokenTest = Faker.StringFaker.Alpha(10);
        var refreshToken = Faker.StringFaker.Alpha(10);

        var signInResponse = new SignInResult
        {
            Token = tokenTest,
            RefreshToken = refreshToken,
            TokenExpiration = 0
        };
        
        

        _authenticationServiceMock
            .Setup(x => x.SignIn(It.IsAny<SignInRequestDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new SignInResponseDto()
            {
                Token = tokenTest,
                RefreshToken = refreshToken,
                TokenExpiration = 0
            });

        // Act
        var result = await _handler.Handle(signInCommand, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(signInResponse.Token, result.Token);
        Assert.Equal(signInResponse.RefreshToken, result.RefreshToken);
        Assert.Equal(signInResponse.TokenExpiration, result.TokenExpiration);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyResult_WhenSignInFails()
    {
        // Arrange
        var signInCommand = new SignInCommand
        {
            Code = "testCode",
            Password = "testPassword",
            Key = "testKey"
        };

        _authenticationServiceMock
            .Setup(x => x.SignIn(It.IsAny<SignInRequestDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new SignInResponseDto());

        // Act
        var result = await _handler.Handle(signInCommand, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(String.IsNullOrEmpty(result.Token));
        Assert.Null(String.IsNullOrEmpty(result.RefreshToken));
        _messageHandlerServiceMock.Verify(x => x.AddError(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenSignInThrowsException()
    {
        // Arrange
        var signInCommand = new SignInCommand
        {
            Code = "testCode",
            Password = "testPassword",
            Key = "testKey"
        };

        _authenticationServiceMock
            .Setup(x => x.SignIn(It.IsAny<SignInRequestDto>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Test exception"));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(signInCommand, CancellationToken.None));
        Assert.Equal("Test exception", exception.Message);
        _messageHandlerServiceMock.Verify(x => x.AddError(), Times.Once);
    }
}