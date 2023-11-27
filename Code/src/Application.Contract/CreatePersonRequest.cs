using Framework.Command;

namespace Application.Contract;

public class CreatePersonRequest : IRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}