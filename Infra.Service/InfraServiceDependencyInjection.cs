using System.Text.Json;
using Application.Mapper;
using Application.Services.Authentication;
using Application.Services.Customer;
using Authentication.Shared.Common;
using Infra.Service.Clients.Authentication;
using Infra.Service.Clients.Customer;
using Infra.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            // .AddMiddlewares()
            .AddConfiguration(configuration)
            .AddExternalServices()
            .AddGlobalConfiguration();
            // .AddCacheService(configuration);

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services) =>
        services
            .AddTransient<IAuthenticationService, AuthenticationService>()
            .AddTransient<ICustomerService, CustomerService>();

    //#TODO: Make implementation of middlewares
    // public static IServiceCollection AddMiddlewares(this IServiceCollection services) =>
    //     services.AddTransient<RequestBodyLoggingMiddleware>()
    //         .AddTransient<ResponseBodyLoggingMiddleware>()
    //         .AddSingleton<ITelemetryInitializer, TelemetryMiddleware>();

    // public static IApplicationBuilder UseRequestBodyLogging(this IApplicationBuilder builder)
    // {
    //     return builder.UseMiddleware<RequestBodyLoggingMiddleware>();
    // }

    // public static IApplicationBuilder UseResponseBodyLogging(this IApplicationBuilder builder)
    // {
    //     return builder.UseMiddleware<ResponseBodyLoggingMiddleware>();
    // }

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
        
        return services;
    }

    private static IServiceCollection
        AddConfiguration(this IServiceCollection services, IConfiguration configuration) =>
        services
            .Configure<AuthenticationConfig>(config =>
            {
                config.BaseUrl = Environment.GetEnvironmentVariable("AuthenticationConfig_BaseUrl") ?? configuration[$"{nameof(AuthenticationConfig)}:BaseUrl"];
            })
            .Configure<CustomerConfig>(configuration.GetSection(nameof(CustomerConfig)));

    private static IServiceCollection AddGlobalConfiguration(this IServiceCollection services)
        => services.AddSingleton(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        });
}