using DNQ.DataFeed.Application.Common.Exceptions;
using DNQ.DataFeed.Domain.Common.Interfaces;
using MediatR;

namespace DNQ.DataFeed.Application.Accounts.Commands.DeleteAccount;

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand>
{
    private readonly IAccountRepo _accountRepo;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteAccountCommandHandler(IAccountRepo accountRepo, IUnitOfWork unitOfWork)
    {
        _accountRepo = accountRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepo.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (account == null) 
        {
            throw new NotFoundException($"The account '{request.Id}' isn't found.");
        }

        await _accountRepo.RemoveAccount(account);
        await _unitOfWork.CommitChangesAsync(cancellationToken);
    }
}
