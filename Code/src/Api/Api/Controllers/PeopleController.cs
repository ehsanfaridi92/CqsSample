using Framework.Command;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PeopleController : ControllerBase
{
    private readonly IRequestBus _requestBus;
    public PeopleController(IRequestBus requestBus)
    {
        _requestBus = requestBus;
    }

    //[HttpPost]
    //public async Task<IActionResult> Create([FromBody] CreatePersonRequest request)
    //{
    //    var command = new CreatePersonCommand()
    //    {
    //        FirstName = request.FirstName,
    //        LastName = request.LastName
    //    };

    //    await _requestBus.Dispatch(command);

    //    return Ok();
    //}
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonRequest request, CancellationToken cancellationToken = default)
    {
        var personRequest = new Application.Contract.CreatePersonRequest()
        {
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var id = await _requestBus.Dispatch<Application.Contract.CreatePersonRequest, long>(personRequest, cancellationToken);

        return Ok(id);
    }
}