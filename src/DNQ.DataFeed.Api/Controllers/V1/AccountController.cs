using DNQ.DataFeed.Application.Accounts.Commands.CreateAccount;
using DNQ.DataFeed.Application.Accounts.Commands.DeleteAccount;
using DNQ.DataFeed.Application.Accounts.Commands.UpdateAccount;
using DNQ.DataFeed.Application.Accounts.Queries.GetAccount;
using DNQ.DataFeed.Application.Accounts.Queries.ListAccounts;
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

    [HttpGet(ApiEndPoint.V1.Accounts.List)]
    public async Task<IActionResult> List([FromQuery] ListAccountsCommand request, CancellationToken cancellationToken)
    {
        var dto = await _mediator.Send(request);

        return Ok(dto);
    }

    [HttpPost(ApiEndPoint.V1.Accounts.Update)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        request.Id = id;
        await _mediator.Send(request);

        return Ok();
    }

    [HttpDelete(ApiEndPoint.V1.Accounts.Delete)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteAccountCommand(id));

        return Ok();
    }
}
