using DNQ.DataFeed.Domain.Common.Interfaces;
using DNQ.DataFeed.Domain.Sites;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace DNQ.DataFeed.Application.Sites.Commands.CreateSite;

public class CreateSiteCommandHandler : IRequestHandler<CreateSiteCommand, Guid>
{
    private readonly ISiteRepo _siteRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly SiteManager _siteManager;
    public CreateSiteCommandHandler(ISiteRepo siteRepo, SiteManager siteManager, IUnitOfWork unitOfWork)
    {
        _siteRepo = siteRepo;
        _siteManager = siteManager;
        _unitOfWork = unitOfWork;
    }
    public async Task<Guid> Handle(CreateSiteCommand request, CancellationToken cancellationToken)
    {
        /* create domain object with domain service  */
        Site site = await _siteManager.CreateAsync(request.Code, request.Name);

        /* repo stores domain object */
        await _siteRepo.AddSite(site);
        await _unitOfWork.CommitChangesAsync();


        return site.Id;
    }
}
