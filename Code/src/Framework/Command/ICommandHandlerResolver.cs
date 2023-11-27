namespace Framework.Command;

public interface ICommandHandlerResolver
{
    IEnumerable<ICommandHandler<T>> ResolveHandlers<T>(T command) where T : ICommand;
}