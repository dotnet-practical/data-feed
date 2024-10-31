using DNQ.DataFeed.Domain.Common.Interfaces;
using DNQ.DataFeed.Persistence.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DNQ.DataFeed.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
    {
        // Add DbContext using Mysql provider
        services.AddDbContext<AppDbContext>(options =>
           options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
           ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))));

        services.AddScoped<ISiteRepo, SiteRepo>();
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<AppDbContext>());

        return services;
    }

}
