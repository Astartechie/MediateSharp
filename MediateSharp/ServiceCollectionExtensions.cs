using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MediateSharp;

public static class ServiceCollectionExtensions
{
    private static readonly Type RequestHandlerType = typeof(IRequestHandler<,>);

    public static IServiceCollection AddMediateSharp(this IServiceCollection serviceCollection, params Assembly[] assemblies)
    {
        serviceCollection.AddSingleton<ISender, Sender>();

        foreach (var assembly in assemblies)
        {
            serviceCollection.AddRequestHandlers(assembly);
        }

        return serviceCollection;
    }

    public static IServiceCollection AddRequestHandlers(this IServiceCollection serviceCollection, Assembly assembly)
    {
        foreach (var handlerType in assembly.GetTypes().Where(IsValidType))
        {
            serviceCollection.AddRequestHandler(handlerType);
        }

        return serviceCollection;
    }

    public static IServiceCollection AddRequestHandler(this IServiceCollection serviceCollection, Type handlerType)
    {
        foreach (var interfaceType in handlerType.GetInterfaces().Where(IsRequestHandler))
        {
            serviceCollection.AddTransient(interfaceType, handlerType);
        }

        return serviceCollection;
    }

    private static bool IsRequestHandler(Type type)
        => type.IsGenericType && type.GetGenericTypeDefinition() == RequestHandlerType;

    private static bool IsValidType(Type type)
        => type is { IsInterface: false, IsAbstract: false, IsGenericType: false } && type.GetInterfaces().Any(IsRequestHandler);
}