using Autofac;
using Framework.Command;

namespace Infrastructure.Config;

public class AutofacRequestHandlerResolver : IRequestHandlerResolver
{
    private readonly IComponentContext _context;
    public AutofacRequestHandlerResolver(IComponentContext context)
    {
        _context = context;
    }
    public IRequestHandler<TRequest, TResponse> ResolveHandler<TRequest, TResponse>(TRequest request) where TRequest : IRequest
    {
        return _context.Resolve<IRequestHandler<TRequest, TResponse>>();
    }
}