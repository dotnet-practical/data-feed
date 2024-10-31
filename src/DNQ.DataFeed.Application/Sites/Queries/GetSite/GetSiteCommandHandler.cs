using DNQ.DataFeed.Application.Common.Exceptions;
using DNQ.DataFeed.Application.Sites.Queries.Dtos;
using DNQ.DataFeed.Domain.Common.Interfaces;
using MediatR;

namespace DNQ.DataFeed.Application.Sites.Queries.GetSite;

public class GetSiteCommandHandler : IRequestHandler<GetSiteCommand, SiteDto>
{
    private readonly ISiteRepo _siteRepo;
    public GetSiteCommandHandler(ISiteRepo siteRepo)
    {
        _siteRepo = siteRepo;
    }
    public async Task<SiteDto> Handle(GetSiteCommand request, CancellationToken cancellationToken)
    {
        var site = await _siteRepo.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (site == null)
        {
            throw new NotFoundException($"The site '{request.Id}' isn't found.");
        }

        return new SiteDto { Id = request.Id, Code = site.Code, Name = site.Name };
    }
}
