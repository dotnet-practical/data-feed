using DNQ.DataFeed.Domain.Sites;
using Microsoft.Extensions.DependencyInjection;

namespace DNQ.DataFeed.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddTransient<ISiteManager, SiteManager>();

        return services;
    }
}
