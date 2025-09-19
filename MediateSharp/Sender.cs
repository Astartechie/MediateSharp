using Microsoft.Extensions.DependencyInjection;

namespace MediateSharp;

internal class Sender(IServiceProvider serviceProvider) : ISender
{
    public Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest<TResponse>
        => serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>().HandleAsync(request, cancellationToken);
}