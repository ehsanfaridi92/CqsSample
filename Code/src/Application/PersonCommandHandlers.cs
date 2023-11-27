using Application.Contract;
using Domain;
using Framework.Command;

namespace Application;

public class PersonCommandHandlers :
    ICommandHandler<CreatePersonCommand>
    
{
    private readonly IPersonRepository _personRepository;


    public PersonCommandHandlers(IPersonRepository personRepository)
    {
        _personRepository = personRepository;

    }

    public async Task Handle(CreatePersonCommand command)
    {
        var person=new Person()
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
        };

        await _personRepository.Create(person);

    }

}