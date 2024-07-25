// using Application.Interfaces;
// using ArtesianWell.UnitTest.Infra.Services.Repositories.Budget.context;
// using Domain.Entities.Budget;
// using Infra.Service.Context;
// using Infra.Service.Interfaces;
// using Infra.Service.Repositories;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.ChangeTracking;
// using Moq;
// using Shared.Messages;
//
// namespace ArtesianWell.UnitTest.Infra.Services.Repositories.Budget;
//
// public class BudgetRepositoryTests
// {
//     private readonly Mock<TestableServiceDBContext> _mockContext;
//     private readonly Mock<IMessageHandlerService> _mockMessageHandlerService;
//     private readonly Mock<IUnitOfWork> _mockUnitOfWork;
//     private readonly BudgetRepository _repository;
//     private readonly Bogus.Faker _faker;
//     private readonly Mock<IServiceDBContext> _mockServiceDBContext;
//
//     public BudgetRepositoryTests()
//     {
//         _mockContext = new Mock<TestableServiceDBContext>();
//         _mockMessageHandlerService = new Mock<IMessageHandlerService>();
//         _mockServiceDBContext = new Mock<IServiceDBContext>();
//         _mockUnitOfWork = new Mock<IUnitOfWork>();
//         _repository = new BudgetRepository(_mockServiceDBContext.Object, _mockMessageHandlerService.Object, _mockUnitOfWork.Object);
//         _faker = new Bogus.Faker();
//     }
//
//     [Fact]
//     public async Task Insert_Success_ReturnsBudgetEntity()
//     {
//         // Arrange
//         var fakeBudget = new BudgetEntity
//         {
//             Id = _faker.Random.Guid().ToString(),
//             Status = _faker.Random.String(),
//             DateAccepted = _faker.Date.Future(),
//             DateChoose = _faker.Date.Future(),
//             DescriptionService = _faker.Lorem.Sentence(),
//             TotalValue = _faker.Random.Decimal()
//         };
//         
//         var mockDbSet = new Mock<DbSet<BudgetEntity>>();
//         _mockServiceDBContext.Setup(x => x.Budgets).Returns(mockDbSet.Object);
//
//         // mockDbSet.Setup(x => x.AddAsync(It.IsAny<BudgetEntity>(), It.IsAny<CancellationToken>()))
//         //     .ReturnsAsync(new EntityEntry<BudgetEntity>(new Mock<EntityEntry>().Object);
//         
//         // Act
//         var result = await _repository.Insert(fakeBudget, CancellationToken.None);
//
//         // Assert
//         mockDbSet.Verify(x => x.AddAsync(It.IsAny<BudgetEntity>(), It.IsAny<CancellationToken>()), Times.Once);
//         _mockUnitOfWork.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);
//         Assert.NotNull(result);
//         Assert.Equal(fakeBudget, result);
//     }
// }
