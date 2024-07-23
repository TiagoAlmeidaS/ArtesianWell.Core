using Domain.Entities.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Service.Configurations.Models;

public class ServiceModelConfiguration: IEntityTypeConfiguration<ServiceEntity>
{
    public void Configure(EntityTypeBuilder<ServiceEntity> builder)
    {
        builder.HasKey(cm => cm.Id);
        builder.Property(cm => cm.Available).IsRequired();
        builder.Property(cm => cm.Description).IsRequired();
        builder.Property(cm => cm.Title).IsRequired();
        builder.Property(medals => medals.UpdatedAt).HasDefaultValueSql("now()");
        builder.Property(medals => medals.CreatedAt).HasDefaultValueSql("now()");
    }
}