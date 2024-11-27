using DNQ.DataFeed.Application.Sites.Queries.Dtos;
using DNQ.DataFeed.Domain.Common.Interfaces;
using MediatR;

namespace DNQ.DataFeed.Application.Sites.Queries.ListSitesPaging;

public class ListSitesPagingCommandHandler : IRequestHandler<ListSitesPagingCommand, ListSitesPagingDto>
{
    private readonly ISiteRepo _siteRepo;
    public ListSitesPagingCommandHandler(ISiteRepo siteRepo)
    {
        _siteRepo = siteRepo;
    }

    public async Task<ListSitesPagingDto> Handle(ListSitesPagingCommand request, CancellationToken cancellationToken)
    {
        request.AllocateDefaultValueIfRequired();

        var predicate = SiteSpecifications.Filter(request.Code, request.Name);
        var sites = await _siteRepo.ListAsync(predicate, request.Sort, request.Page, request.PageSize);
        var siteDtos = sites.Select(x => new SiteDto { Id = x.Id, Name = x.Name, Code = x.Code }).ToList();
        var totals = await _siteRepo.CountAsync(predicate);

        return new ListSitesPagingDto(siteDtos, totals, request.Page, request.PageSize);
    }
}
