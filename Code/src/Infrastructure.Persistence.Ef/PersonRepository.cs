using Domain;

namespace Infrastructure.Persistence.Ef;

public class PersonRepository : IPersonRepository
{
    PeopleDbContext _dbContext;

    public PersonRepository(PeopleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(Person person)
    {
        await _dbContext.Set<Person>()
            .AddAsync(person);
        await _dbContext.SaveChangesAsync();
    }

   
}