using Autofac;
using Framework.Command;

namespace Infrastructure.Config;

public class AutofacCommandHandlerResolver : ICommandHandlerResolver
{
    private readonly IComponentContext _context;

    public AutofacCommandHandlerResolver(IComponentContext context)
    {
        _context = context;
    }
    public IEnumerable<ICommandHandler<T>> ResolveHandlers<T>(T command) where T : ICommand
    {
        return _context.Resolve<IEnumerable<ICommandHandler<T>>>();
    }
}