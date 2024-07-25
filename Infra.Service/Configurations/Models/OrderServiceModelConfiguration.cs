using Domain.Entities.OrderService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Service.Configurations.Models;

public class OrderServiceModelConfiguration: IEntityTypeConfiguration<OrderServiceEntity>
{
    public void Configure(EntityTypeBuilder<OrderServiceEntity> builder)
    {
        builder.HasKey(cm => cm.Id);
        builder.Property(cm => cm.Status).IsRequired();
        builder.Property(cm => cm.ClientId).IsRequired();
        builder.Property(cm => cm.ServiceId).IsRequired();
        builder.Property(cm => cm.BudgetSchedulingDate).IsRequired().HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property(os => os.ServiceSchedulingDate).IsRequired().HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        builder.Property(os => os.UpdatedAt)
            .HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
            .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

        builder.Property(os => os.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");
    }
}