namespace Framework.Command;

public interface ICommandBus
{
    Task Dispatch<T>(T command) where T : class, ICommand;
}