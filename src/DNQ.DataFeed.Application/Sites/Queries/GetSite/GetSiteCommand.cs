using DNQ.DataFeed.Application.Sites.Queries.Dtos;
using MediatR;

namespace DNQ.DataFeed.Application.Sites.Queries.GetSite;

public class GetSiteCommand: IRequest<SiteDto>
{
    public Guid Id { get; set; }

    public GetSiteCommand(Guid id)
    {
        Id = id;
    }
}
