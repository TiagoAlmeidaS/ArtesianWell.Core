using System.Reflection;
using Application.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace Application;


public static class ApplicationDependencyInjection
{
    public static IServiceCollection ApplicationExtension(this IServiceCollection services) => services
        .AddUseCases()
        .AddMapper();
    
    private static IServiceCollection AddUseCases(this IServiceCollection services) => services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()); });
    
    public static IServiceCollection AddMapper(this IServiceCollection services) => 
        services
            .AddAutoMapper(typeof(AuthenticationProfile))
            .AddAutoMapper(typeof(CustomerProfile));
}