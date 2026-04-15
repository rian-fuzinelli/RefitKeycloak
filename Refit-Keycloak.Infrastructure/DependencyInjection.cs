using Microsoft.Extensions.DependencyInjection;
using Refit;
using RefitKeycloak.Infrastructure.External;

namespace RefitKeycloak.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddRefitClient<IExternalApi>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
            });

        return services;
    }
}
