using MediatR;

namespace DNQ.DataFeed.Application.Sites.Commands.DeleteSite;

public class DeleteSiteCommand: IRequest
{
    public Guid Id { get; set; }

    public DeleteSiteCommand(Guid id)
    {
        Id = id;
    }
}
