using DNQ.DataFeed.Application.Sites.Queries.Dtos;
using DNQ.DataFeed.Domain.Common.Interfaces;
using DNQ.DataFeed.Domain.Sites;
using MediatR;
using System.Linq.Expressions;

namespace DNQ.DataFeed.Application.Sites.Queries.ListSites;

public class ListSitesCommandHandler : IRequestHandler<ListSitesCommand, List<SiteDto>>
{
    private readonly ISiteRepo _siteRepo;
    public ListSitesCommandHandler(ISiteRepo siteRepo)
    {
        _siteRepo = siteRepo;
    }

    public async Task<List<SiteDto>> Handle(ListSitesCommand request, CancellationToken cancellationToken)
    {
        Expression<Func<Site, bool>> predicate = PredicateBuilder.True<Site>();

        if (!string.IsNullOrEmpty(request.Code))
        {
            predicate = predicate.And(x => x.Code.Contains(request.Code));
        }

        if (!string.IsNullOrEmpty(request.Name))
        {
            predicate = predicate.And(x => x.Name.Contains(request.Name));
        }

        var sites = await _siteRepo.ListAsync(predicate, request.Sort);

        return sites.Select(x => new SiteDto { Id = x.Id, Name = x.Name, Code = x.Code }).ToList(); 
    }
}
