using Microsoft.Extensions.DependencyInjection;
using RefitKeycloak.Application.Interfaces;
using RefitKeycloak.Application.Services;

namespace RefitKeycloak.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPostService, PostService>();
        return services;
    }
}
