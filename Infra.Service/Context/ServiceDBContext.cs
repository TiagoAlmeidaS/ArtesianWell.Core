using Domain.Entities.OrderService;
using Domain.Entities.OrderStatus;
using Domain.Entities.Service;
using Infra.Service.Configurations.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Service.Context;

public class ServiceDBContext: DbContext
{
    public DbSet<ServiceEntity> Services => Set<ServiceEntity>();
    public DbSet<OrderServiceEntity> OrderServices => Set<OrderServiceEntity>();
    public DbSet<OrderStatusEntity> OrderStatus => Set<OrderStatusEntity>();

    public ServiceDBContext(DbContextOptions<ServiceDBContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ServiceModelConfiguration());
        builder.ApplyConfiguration(new OrderServiceModelConfiguration());
        builder.ApplyConfiguration(new OrderStatusServiceModelConfiguration());
    }
}