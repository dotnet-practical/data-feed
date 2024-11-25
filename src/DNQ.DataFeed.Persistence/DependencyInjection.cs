using DNQ.DataFeed.Domain.Common.Interfaces;
using DNQ.DataFeed.Persistence.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DNQ.DataFeed.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionStrings:DefaultConnection"];

        // Add DbContext using Mysql provider
        services.AddDbContext<AppDbContext>(options =>
           options.UseMySql(connectionString,
           ServerVersion.AutoDetect(connectionString)));

        services.AddScoped<ISiteRepo, SiteRepo>();
        services.AddScoped<IAccountRepo, AccountRepo>();
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<AppDbContext>());

        return services;
    }

}
