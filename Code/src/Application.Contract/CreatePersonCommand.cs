using Framework.Command;

namespace Application.Contract;


public class CreatePersonCommand : ICommand
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}