using Application.Contract;
using Domain;
using Framework.Command;

namespace Application;

public class PersonRequestHandlers :
    IRequestHandler<CreatePersonRequest,long>
    
{
    private readonly IPersonRepository _personRepository;


    public PersonRequestHandlers(IPersonRepository personRepository)
    {
        _personRepository = personRepository;

    }

    public async Task<long> Handle(CreatePersonRequest request, CancellationToken cancellationToken)
    {
        var person = new Person()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
        };

        await _personRepository.Create(person);

        return person.Id;
    }
}