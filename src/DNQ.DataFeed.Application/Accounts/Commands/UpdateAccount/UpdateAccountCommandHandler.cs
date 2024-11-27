using DNQ.DataFeed.Application.Common.Exceptions;
using DNQ.DataFeed.Domain.Accounts;
using DNQ.DataFeed.Domain.Common.Interfaces;
using MediatR;

namespace DNQ.DataFeed.Application.Accounts.Commands.UpdateAccount;

public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand>
{
    private readonly IAccountRepo _accountRepo;
    private readonly IAccountManager _accountManager;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateAccountCommandHandler(IAccountRepo accountRepo, IAccountManager accountManager, IUnitOfWork unitOfWork)
    {
        _accountRepo = accountRepo;
        _accountManager = accountManager;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepo.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (account == null) 
        {
            throw new NotFoundException($"The account '{request.Id}' isn't found.");
        }

        await _accountManager.UpdateAsync(account, request.PlatformId, request.SiteId, request.InternalId, request.ReferenceValue, request.StartDate, request.EndDate, request.FinYear);
        await _accountRepo.UpdateAccount(account);
        await _unitOfWork.CommitChangesAsync(cancellationToken);
    }
}
