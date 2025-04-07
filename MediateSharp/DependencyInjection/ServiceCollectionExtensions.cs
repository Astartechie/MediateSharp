using MediateSharp.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace MediateSharp.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMediateSharp(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ISender, Sender>();

        return serviceCollection;
    }
}