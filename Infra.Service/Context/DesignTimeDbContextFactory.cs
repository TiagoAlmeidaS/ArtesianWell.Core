using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infra.Service.Context;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ServiceDBContext>
{
    public ServiceDBContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<ServiceDBContext>();
        var connectionString = configuration.GetConnectionString("DB");

        builder.UseNpgsql(connectionString);

        return new ServiceDBContext(builder.Options);
    }
}