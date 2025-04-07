namespace MediateSharp;

public interface ISender
{
    Task SendAsync<TRequest>(TRequest request) where TRequest : IRequest;
    Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>;
}