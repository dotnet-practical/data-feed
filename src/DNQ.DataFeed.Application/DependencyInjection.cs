using DNQ.DataFeed.Application.Sites.Commands.CreateSite;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using DNQ.DataFeed.Domain;

namespace DNQ.DataFeed.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
        });

        return services;
    }
}
