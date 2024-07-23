using System.Text.Json;
using Application.Interfaces;
using Application.Mapper;
using Application.Services.Authentication;
using Application.Services.Customer;
using Authentication.Shared.Common;
using Domain.Repositories;
using Infra.Service.Clients.Authentication;
using Infra.Service.Clients.Customer;
using Infra.Service.Configurations;
using Infra.Service.Context;
using Infra.Service.Repositories;
using Infra.Service.Services;
using Infra.Service.Unit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infra.Service;

public static class InfraServiceDependencyInjection
{
    public static IServiceCollection InfraServiceExtension(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddServices()
            .AddAutomapperProfiles()
            .AddConfiguration(configuration)
            .AddExternalServices()
            .AddGlobalConfiguration()
            .AddDbConnection(configuration)
            .AddRepositories();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services) =>
        services
            .AddTransient<IAuthenticationService, AuthenticationService>()
            .AddTransient<ICustomerService, CustomerService>();

    public static IServiceCollection AddAutomapperProfiles(this IServiceCollection services) =>
        services
            .AddAutoMapper(typeof(AuthenticationProfile));

    private static IServiceCollection AddExternalServices(this IServiceCollection services)
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        var scopedProvider = scope.ServiceProvider;
        var authenticationConfig = scopedProvider.GetRequiredService<IOptionsSnapshot<AuthenticationConfig>>();

        services.AddHttpClient(AuthenticationConst.GetAuthenticationNameApi, client =>
        {
            client.DefaultRequestHeaders.Add(CommonConsts.Headers.Accept,
                CommonConsts.Headers.AcceptJsonContentValue);
            client.BaseAddress = new Uri(authenticationConfig.Value.BaseUrl);
        });
        
        var customerConfig = scopedProvider.GetRequiredService<IOptionsSnapshot<CustomerConfig>>();
        services.AddHttpClient(CustomerConst.GetApiName, client =>
        {
            client.DefaultRequestHeaders.Add(CommonConsts.Headers.Accept,
                CommonConsts.Headers.AcceptJsonContentValue);
            client.BaseAddress = new Uri(customerConfig.Value.BaseUrl);
        });
        
        return services;
    }

    private static IServiceCollection
        AddConfiguration(this IServiceCollection services, IConfiguration configuration) =>
        services
            .Configure<AuthenticationConfig>(configuration.GetSection(nameof(AuthenticationConfig)))
            .Configure<CustomerConfig>(configuration.GetSection(nameof(CustomerConfig)))
            .Configure<ServiceDBContext>(configuration.GetSection(nameof(ServiceDBContext)))
            .Configure<ServicesDbConfig>(configuration.GetSection(nameof(ServicesDbConfig)));

    private static IServiceCollection AddGlobalConfiguration(this IServiceCollection services)
        => services.AddSingleton(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        });
    
    private static IServiceCollection AddDbConnection(this IServiceCollection services, IConfiguration configuration)
    {
        var scope = services.BuildServiceProvider().CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var customerDbConfig = serviceProvider.GetRequiredService<IOptionsSnapshot<ServicesDbConfig>>();

        if (customerDbConfig.Value.UseInMemoryDatabase)
        {
            services.AddDbContextPool<ServiceDBContext>(options =>
                options.UseInMemoryDatabase("TestingDB"));
        }
        else
        {
            services.AddDbContextPool<ServiceDBContext>(options => options.UseNpgsql(configuration.GetConnectionString("DB"))
                .EnableSensitiveDataLogging() 
                .LogTo(Console.WriteLine, LogLevel.Information));
            ;
        }

        return services;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services
            .AddTransient<IServiceRepository, ServiceRepository>()
            .AddTransient<IUnitOfWork, UnitOfWorkService>();
}