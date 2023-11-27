namespace Framework.Command;

public interface IRequestBus
{
    Task<TResponse> Dispatch<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken) where TRequest : class, IRequest;
}