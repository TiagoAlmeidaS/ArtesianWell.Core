using Domain.Entities.Budget;
using Domain.Entities.OrderService;
using Domain.Entities.OrderStatus;
using Domain.Entities.Service;
using Microsoft.EntityFrameworkCore;

namespace Infra.Service.Interfaces;

public interface IServiceDBContext
{
    DbSet<ServiceEntity> Services { get; }
    DbSet<OrderServiceEntity> OrderServices { get; }
    DbSet<OrderStatusEntity> OrderStatus { get; }
    DbSet<BudgetEntity> Budgets { get; }
}