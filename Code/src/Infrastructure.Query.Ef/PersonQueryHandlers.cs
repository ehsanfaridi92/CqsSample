using Framework.Query;
using Infrastructure.Query.Ef.Models;

namespace Infrastructure.Query.Ef;

public class PersonQueryHandlers :
    IQueryHandler<GetPersonRequest, Person>
{
    private readonly PeopleQueryDbContext _dbContext;

    public PersonQueryHandlers(PeopleQueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Person> Handle(GetPersonRequest request)
    {
        return new Person
        {
            FirstName = "John",
            LastName = "Doe",
            Id = 1
        };
        //return await _dbContext.Set<Person>()
        //    .FirstOrDefaultAsync(x => x.Id == request.Id);
    }
}