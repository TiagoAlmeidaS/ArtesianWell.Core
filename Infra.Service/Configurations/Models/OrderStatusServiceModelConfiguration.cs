using Domain.Entities.OrderStatus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Service.Configurations.Models;

public class OrderStatusServiceModelConfiguration: IEntityTypeConfiguration<OrderStatusEntity>
{
    public void Configure(EntityTypeBuilder<OrderStatusEntity> builder)
    {
        builder.HasKey(os => os.Id);
        builder.Property(os => os.Name).IsRequired();
        builder.Property(os => os.Description).IsRequired();
        builder.Property(os => os.PossibleActions).IsRequired();
    }
}