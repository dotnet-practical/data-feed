using DNQ.DataFeed.Application.Sites.Commands.CreateSite;
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
}
