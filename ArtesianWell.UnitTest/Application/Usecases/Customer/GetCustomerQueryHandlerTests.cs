using Application.Services.Customer;
using Application.Services.Customer.Dto;
using Application.Usecases.Customer.Query.GetCustomer;
using Moq;
using Shared.Dto;
using Shared.Messages;

namespace ArtesianWell.UnitTest.Application.Usecases.Customer;

public class GetCustomerQueryHandlerTests
{
    private readonly Mock<ICustomerService> _mockCustomerService;
    private readonly Mock<IMessageHandlerService> _mockMessageHandlerService;
    private readonly GetCustomerQueryHandler _handler;
    private readonly Bogus.Faker _faker;

    public GetCustomerQueryHandlerTests()
    {
        _mockCustomerService = new Mock<ICustomerService>();
        _mockMessageHandlerService = new Mock<IMessageHandlerService>();
        _handler = new GetCustomerQueryHandler(_mockCustomerService.Object, _mockMessageHandlerService.Object);
        _faker = new Bogus.Faker();
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldReturnCustomerResult()
    {
        // Arrange
        var request = new GetCustomerQuery
        {
            Email = _faker.Internet.Email(),
            Document = _faker.Random.AlphaNumeric(10)
        };

        var expectedResponse = new GetCustomerResponse
        {
            Name = _faker.Name.FullName(),
            Email = request.Email,
            Document = request.Document,
            Number = _faker.Phone.PhoneNumber(),
            ProfileType = _faker.Random.Int(1, 5)
        };

        _mockCustomerService.Setup(x => x.GetCustomer(It.IsAny<GetCustomerRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ApiResponse<GetCustomerResponse>.Success(expectedResponse));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.Name, result.Name);
        Assert.Equal(expectedResponse.Email, result.Email);
        Assert.Equal(expectedResponse.Document, result.Document);
        Assert.Equal(expectedResponse.Number, result.Number);
        Assert.Equal(expectedResponse.ProfileType, result.ProfileType);
    }

    [Fact]
    public async Task Handle_GivenInvalidRequest_ShouldReturnNull()
    {
        // Arrange
        var request = new GetCustomerQuery
        {
            Email = _faker.Internet.Email(),
            Document = _faker.Random.AlphaNumeric(10)
        };

        _mockCustomerService.Setup(x => x.GetCustomer(It.IsAny<GetCustomerRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ApiResponse<GetCustomerResponse>.Error(new ApiError()
            {
                ErrorCode = 400,
                ErrorMessage = "Error"
            }));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }
}