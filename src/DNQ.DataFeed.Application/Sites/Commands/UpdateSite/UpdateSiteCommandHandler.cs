using DNQ.DataFeed.Application.Common.Exceptions;
using DNQ.DataFeed.Domain.Common.Interfaces;
using DNQ.DataFeed.Domain.Sites;
using MediatR;

namespace DNQ.DataFeed.Application.Sites.Commands.UpdateSite;

public class UpdateSiteCommandHandler : IRequestHandler<UpdateSiteCommand>
{
    private readonly ISiteRepo _siteRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISiteManager _siteManager;

    public UpdateSiteCommandHandler(ISiteRepo siteRepo, ISiteManager siteManager, IUnitOfWork unitOfWork)
    {
        _siteRepo = siteRepo;
        _siteManager = siteManager;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(UpdateSiteCommand request, CancellationToken cancellationToken)
    {
        Site updateSite = await _siteRepo.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (updateSite == null)
        {
           throw new NotFoundException($"The site '{request.Id}' isn't found.");
        }

        await _siteManager.UpdateAsync(updateSite, request.Code, request.Name);

        /* repo stores domain object */
        await _siteRepo.UpdateSite(updateSite);
        await _unitOfWork.CommitChangesAsync();
    }
}
