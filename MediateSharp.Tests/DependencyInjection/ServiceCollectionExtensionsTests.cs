using MediateSharp.DependencyInjection;
using MediateSharp.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace MediateSharp.Tests.DependencyInjection;

[TestClass]
public class ServiceCollectionExtensionsTests
{
    [TestMethod]
    [DataRow(typeof(ISender), typeof(Sender), ServiceLifetime.Singleton)]
    public void AddMediateSharp_Registers_Types(Type interfaceType, Type classType, ServiceLifetime serviceLifetime)
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddMediateSharp();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Act
        var service = serviceProvider.GetService(interfaceType);

        // Assert
        Assert.IsNotNull(service);

        var serviceDescriptor = serviceCollection.SingleOrDefault(d => d.ServiceType == interfaceType && d.ImplementationType == classType && d.Lifetime == serviceLifetime);
        Assert.IsNotNull(serviceDescriptor);
    }
}