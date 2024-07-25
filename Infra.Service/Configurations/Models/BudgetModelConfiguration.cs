using Domain.Entities.Budget;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Service.Configurations.Models;

public class BudgetModelConfiguration: IEntityTypeConfiguration<BudgetEntity>
{
    public void Configure(EntityTypeBuilder<BudgetEntity> builder)
    {
        builder.HasKey(cm => cm.Id);
        builder.Property(cm => cm.OrderServiceId).IsRequired();
        builder.Property(cm => cm.Status).IsRequired();
        builder.Property(cm => cm.DescriptionService).IsRequired();
        builder.Property(cm => cm.DateChoose).IsRequired().HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property(os => os.DateAccepted).IsRequired().HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property(cm => cm.TotalValue).IsRequired();

        builder.Property(os => os.UpdatedAt)
            .HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
            .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

        builder.Property(os => os.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");
    }
}