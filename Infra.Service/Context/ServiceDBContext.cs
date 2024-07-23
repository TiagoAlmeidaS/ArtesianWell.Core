using Domain.Entities.Service;
using Infra.Service.Configurations.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Service.Context;

public class ServiceDBContext: DbContext
{
    public DbSet<ServiceEntity> Services => Set<ServiceEntity>();

    public ServiceDBContext(DbContextOptions<ServiceDBContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ServiceModelConfiguration());
    }
}