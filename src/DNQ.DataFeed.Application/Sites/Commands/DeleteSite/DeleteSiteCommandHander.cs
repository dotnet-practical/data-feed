using DNQ.DataFeed.Application.Common.Exceptions;
using DNQ.DataFeed.Domain.Common.Interfaces;
using MediatR;

namespace DNQ.DataFeed.Application.Sites.Commands.DeleteSite;

public class DeleteSiteCommandHandler : IRequestHandler<DeleteSiteCommand>
{
    private readonly ISiteRepo _siteRepo;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteSiteCommandHandler(ISiteRepo siteRepo, IUnitOfWork unitOfWork)
    {
        _siteRepo = siteRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteSiteCommand request, CancellationToken cancellationToken)
    {
        var deleteSite = await _siteRepo.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (deleteSite == null) 
        {
            throw new NotFoundException($"The site '{request.Id}' isn't found.");
        }

        await _siteRepo.RemoveSite(deleteSite);
        await _unitOfWork.CommitChangesAsync();
    }
}