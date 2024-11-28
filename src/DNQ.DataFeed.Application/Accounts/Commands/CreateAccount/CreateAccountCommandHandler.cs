using DNQ.DataFeed.Domain.Accounts;
using DNQ.DataFeed.Domain.Common.Interfaces;
using MediatR;

namespace DNQ.DataFeed.Application.Accounts.Commands.CreateAccount;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Guid>
{
    private readonly IAccountManager _accountManager;
    private readonly IAccountRepo _accountRepo;
    private readonly IUnitOfWork _unitOfWork;
    public CreateAccountCommandHandler(IAccountManager accountManager, IAccountRepo accountRepo, IUnitOfWork unitOfWork)
    {
        _accountManager = accountManager;
        _accountRepo = accountRepo;
        _unitOfWork = unitOfWork;   
    }
    public async Task<Guid> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        // parse to domain object
        Account account = await _accountManager.CreateAsync(request.PlatformId, request.SiteId, request.InternalId, request.ReferenceValue, request.StartDate, request.EndDate, request.FinYear);

        // save domain object
        await _accountRepo.AddAccount(account);
        await _unitOfWork.CommitChangesAsync(cancellationToken);

        return account.Id;
    }
}
