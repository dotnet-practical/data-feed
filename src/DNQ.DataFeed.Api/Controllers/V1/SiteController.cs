using DNQ.DataFeed.Application.Sites.Commands.CreateSite;
using DNQ.DataFeed.Application.Sites.Commands.DeleteSite;
using DNQ.DataFeed.Application.Sites.Commands.UpdateSite;
using DNQ.DataFeed.Application.Sites.Queries.GetSite;
using DNQ.DataFeed.Application.Sites.Queries.ListSites;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DNQ.DataFeed.Api.Controllers.V1;

[ApiController]
public class SiteController: ControllerBase
{
    private readonly ISender _mediator;
    public SiteController(ISender sender) 
    {
        _mediator = sender;
    }

    /* CRUD */
    [HttpPost(ApiEndPoint.V1.Sites.Create)]
    public async Task<IActionResult> Create([FromBody] CreateSiteCommand request, CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(request);

        return Created("", new { Id = id });
    }

    [HttpPost(ApiEndPoint.V1.Sites.Update)]
    public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] UpdateSiteCommand request, CancellationToken cancellationToken)
    {
        request.Id = id;
        await _mediator.Send(request);

        return Ok();
    }

    [HttpDelete(ApiEndPoint.V1.Sites.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteSiteCommand(id));

        return Ok();
    }

    [HttpGet(ApiEndPoint.V1.Sites.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var dto = await _mediator.Send(new GetSiteCommand(id));

        return Ok(dto);
    }

    [HttpGet(ApiEndPoint.V1.Sites.List)]
    public async Task<IActionResult> List([FromQuery] ListSitesCommand listSitesCommand, CancellationToken cancellationToken)
    {
        var dto = await _mediator.Send(listSitesCommand);

        return Ok(dto);
    }
}
