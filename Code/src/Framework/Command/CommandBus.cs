namespace Framework.Command;
public class CommandBus : ICommandBus
{
    private readonly ICommandHandlerResolver _resolver;

    public CommandBus(ICommandHandlerResolver resolver)
    {
        _resolver = resolver;
    }
    public async Task Dispatch<T>(T command) where T : class, ICommand
    {
        var handlers = _resolver.ResolveHandlers(command).ToList();
        foreach (var handler in handlers)
        {
            await handler.Handle(command);
        }
    }
}
public class RequestBus : IRequestBus
{
    private readonly IRequestHandlerResolver _resolver;

    public RequestBus(IRequestHandlerResolver resolver)
    {
        _resolver = resolver;
    }
    public async Task<TResponse> Dispatch<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken) where TRequest : class, IRequest
    {
        var handler = _resolver.ResolveHandler<TRequest, TResponse>(request);

        return await handler.Handle(request, cancellationToken);
    }
}