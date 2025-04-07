using Microsoft.Extensions.DependencyInjection;

namespace MediateSharp.Implementation;

internal class Sender(IServiceProvider serviceProvider) : ISender
{
    public Task SendAsync<TRequest>(TRequest request) where TRequest : IRequest
    {
        var requestHandler = serviceProvider.GetService<IRequestHandler<TRequest>>();
        if (requestHandler is null) throw new UnknownRequestException();
        return requestHandler.HandleAsync(request);
    }

    public Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>
    {
        var requestHandler = serviceProvider.GetService<IRequestHandler<TRequest, TResponse>>();
        if (requestHandler is null) throw new UnknownRequestException();
        return requestHandler.HandleAsync(request);
    }
}