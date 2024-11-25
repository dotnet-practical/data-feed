using DNQ.DataFeed.Application.Accounts.Commands.CreateAccount;
using DNQ.DataFeed.Application.Accounts.Queries.GetAccount;
using DNQ.DataFeed.Application.Sites.Commands.CreateSite;
using DNQ.DataFeed.Application.Sites.Queries.GetSite;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DNQ.DataFeed.Api.Controllers.V1;

[ApiController]
public class AccountController: ControllerBase
{
    private readonly ISender _mediator;
    public AccountController(ISender sender)
    {
        _mediator = sender;
    }

    /* CRUD */
    [HttpPost(ApiEndPoint.V1.Accounts.Create)]
    public async Task<IActionResult> Create([FromBody] CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(request);

        return Created("", new { Id = id });
    }

    [HttpGet(ApiEndPoint.V1.Accounts.Get)]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var dto = await _mediator.Send(new GetAccountCommand(id));

        return Ok(dto);
    }
}
