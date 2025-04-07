using FakeItEasy;
using MediateSharp.Implementation;

namespace MediateSharp.Tests.Implementation;

[TestClass]
public class SenderTests
{
    [TestMethod]
    public async Task SendAsync_Unknown_Request_Throws_UnknownRequestException()
    {
        // Arrange
        var serviceProvider = A.Fake<IServiceProvider>();
        A.CallTo(() => serviceProvider.GetService(A<Type>.Ignored)).Returns(null);

        var sender = new Sender(serviceProvider);
        var request = A.Fake<IRequest>();

        // Act & Assert
        await Assert.ThrowsExceptionAsync<UnknownRequestException>(() => sender.SendAsync(request));
    }

    [TestMethod]
    public async Task SendAsync_Unknown_Request_With_Response_Throws_UnknownRequestException()
    {
        // Arrange
        var serviceProvider = A.Fake<IServiceProvider>();
        A.CallTo(() => serviceProvider.GetService(A<Type>.Ignored)).Returns(null);

        var sender = new Sender(serviceProvider);
        var request = A.Fake<IRequest<int>>();

        // Act & Assert
        await Assert.ThrowsExceptionAsync<UnknownRequestException>(() => sender.SendAsync<IRequest<int>, int>(request));
    }

    [TestMethod]
    public async Task SendAsync_With_Request_Calls_RequestHandler_HandleAsync()
    {
        // Arrange
        var serviceProvider = A.Fake<IServiceProvider>();
        var requestHandler = A.Fake<IRequestHandler<IRequest>>();

        A.CallTo(() => serviceProvider.GetService(A<Type>.Ignored)).Returns(requestHandler);

        var sender = new Sender(serviceProvider);
        var request = A.Fake<IRequest>();

        // Act
        await sender.SendAsync(request);

        // Assert
        A.CallTo(() => requestHandler.HandleAsync(A<IRequest>.Ignored)).MustHaveHappenedOnceExactly();
    }

    [TestMethod]
    public async Task SendAsync_With_Request_With_Response_Calls_RequestHandler_HandleAsync()
    {
        // Arrange
        var serviceProvider = A.Fake<IServiceProvider>();
        var requestHandler = A.Fake<IRequestHandler<IRequest<int>, int>>();

        A.CallTo(() => serviceProvider.GetService(A<Type>.Ignored)).Returns(requestHandler);

        var sender = new Sender(serviceProvider);
        var request = A.Fake<IRequest<int>>();

        // Act
        await sender.SendAsync<IRequest<int>, int>(request);

        // Assert
        A.CallTo(() => requestHandler.HandleAsync(A<IRequest<int>>.Ignored)).MustHaveHappenedOnceExactly();
    }
}