using Framework.Query;
using Infrastructure.Query.Ef;
using Infrastructure.Query.Ef.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PeopleQueryController : ControllerBase
{
    private readonly IQueryBus _queryBus;

    public PeopleQueryController(IQueryBus queryBus)
    {
        _queryBus = queryBus;
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> Create([FromRoute] long id)
    {
        var query = new GetPersonRequest()
        {
            Id = id
        };

        var person=await _queryBus.Execute<GetPersonRequest,Person>(query);

        return Ok(person);
    }
}