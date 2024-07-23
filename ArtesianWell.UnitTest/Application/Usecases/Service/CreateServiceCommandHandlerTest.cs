using System.Net;
using Application.Usecases.Service.Command.CreateService;
using Bogus;
using Domain.Entities.Service;
using Domain.Repositories;
using Moq;
using Shared.Messages;

namespace ArtesianWell.UnitTest.Application.Usecases.Service;

public class CreateServiceCommandHandlerTests
{
    private readonly Mock<IServiceRepository> _serviceRepositoryMock;
    private readonly Mock<IMessageHandlerService> _msgMock;
    private readonly CreateServiceCommandHandler _handler;

    public CreateServiceCommandHandlerTests()
    {
        _serviceRepositoryMock = new Mock<IServiceRepository>();
        _msgMock = new Mock<IMessageHandlerService>();
        _handler = new CreateServiceCommandHandler(_msgMock.Object, _serviceRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnResult_WhenServiceIsCreatedSuccessfully()
    {
        // Arrange
        var command = new Faker<CreateServiceCommand>()
            .RuleFor(c => c.Title, f => f.Lorem.Sentence())
            .RuleFor(c => c.Description, f => f.Lorem.Paragraph())
            .RuleFor(c => c.Available, f => f.Random.Bool());

        _serviceRepositoryMock
            .Setup(repo => repo.Insert(It.IsAny<ServiceEntity>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(It.IsAny<ServiceEntity>());

        // Act
        var result = await _handler.Handle(command.Generate(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        _serviceRepositoryMock.Verify(repo => repo.Insert(It.IsAny<ServiceEntity>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldAddError_WhenExceptionIsThrown()
    {
        // Arrange
        var command = new Faker<CreateServiceCommand>()
            .RuleFor(c => c.Title, f => f.Lorem.Sentence())
            .RuleFor(c => c.Description, f => f.Lorem.Paragraph())
            .RuleFor(c => c.Available, f => f.Random.Bool());

        var exceptionMessage = "Test exception";

        _serviceRepositoryMock
            .Setup(repo => repo.Insert(It.IsAny<ServiceEntity>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception(exceptionMessage));
        
        _msgMock.Setup(x => x.AddError());

        // Act
        var result = await _handler.Handle(command.Generate(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        _msgMock.Verify(msg => msg.AddError()
            .WithErrorCode(It.IsAny<string>())
            .WithMessage(exceptionMessage)
            .WithStatusCode(HttpStatusCode.BadRequest)
            .WithStackTrace(It.IsAny<string>())
            .Commit(), Times.Once);
    }
}