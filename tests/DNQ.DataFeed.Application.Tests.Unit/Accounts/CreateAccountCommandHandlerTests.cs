using DNQ.DataFeed.Application.Accounts.Commands.CreateAccount;
using DNQ.DataFeed.Domain.Accounts;
using DNQ.DataFeed.Domain.Common.Exceptions;
using DNQ.DataFeed.Domain.Common.Interfaces;
using FluentAssertions;
using NSubstitute;

namespace DNQ.DataFeed.Application.Tests.Unit.Accounts;

public class CreateAccountCommandHandlerTests
{
    private readonly IAccountManager _accountManager;
    private readonly IAccountRepo _accountRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly CreateAccountCommandHandler _sut;
    public CreateAccountCommandHandlerTests()
    {
        _accountRepo = Substitute.For<IAccountRepo>();
        _accountManager = Substitute.For<IAccountManager>();
        _unitOfWork = Substitute.For<IUnitOfWork>();

        _sut = new(_accountManager, _accountRepo, _unitOfWork);
    }

    [Fact]
    public async Task Handle_WhenValidCommand_ReturnAccountGuid()
    {
        // arrange
        var request = new CreateAccountCommand() 
        {
            StartDate = new DateTime(2020, 01,01),
            FinYear = 2020,
            InternalId = Guid.NewGuid(),
            PlatformId = Guid.NewGuid(),
            ReferenceValue = "XXX123",
            SiteId = Guid.NewGuid(),
        };
        var createdAccount = new Account(request.PlatformId, request.SiteId, request.InternalId, request.ReferenceValue, request.StartDate, request.EndDate, request.FinYear);
        _accountManager
            .CreateAsync(request.PlatformId, request.SiteId, request.InternalId, request.ReferenceValue, request.StartDate, request.EndDate, request.FinYear)
            .Returns(Task.FromResult(createdAccount));
        _accountRepo.AddAccount(createdAccount).Returns(Task.CompletedTask);
        _unitOfWork.CommitChangesAsync(CancellationToken.None).Returns(Task.CompletedTask);

        // action
        var createdGuid = await _sut.Handle(request, CancellationToken.None);

        // assertion
        createdAccount.Id.Should().Be(createdGuid);
    }

    [Fact]
    public async Task Handle_DuplicatedInternalId_ThrowException()
    {
        // arrange
        var request = new CreateAccountCommand()
        {
            StartDate = new DateTime(2020, 01, 01),
            FinYear = 2020,
            InternalId = Guid.NewGuid(),
            PlatformId = Guid.NewGuid(),
            ReferenceValue = "XXX123",
            SiteId = Guid.NewGuid(),
        };
   
        _accountManager
            .CreateAsync(request.PlatformId, request.SiteId, request.InternalId, request.ReferenceValue, request.StartDate, request.EndDate, request.FinYear)
            .Returns<Task<Account>>(x => throw new BussinessException("Cannot have two accounts with the same internal id."));

        // action
        Func<Task> act = async () => await _sut.Handle(request, CancellationToken.None);

        // assertion
        await act.Should().ThrowAsync<BussinessException>().WithMessage("Cannot have two accounts with the same internal id.");
    }

    [Fact]
    public async Task Handle_WhenInvalidStartDate_ThrowException()
    {
        // arrange
        var request = new CreateAccountCommand()
        {
            StartDate = new DateTime(2020, 01, 01),
            EndDate = new DateTime(2019, 01, 01),
            FinYear = 2020,
            InternalId = Guid.NewGuid(),
            PlatformId = Guid.NewGuid(),
            ReferenceValue = "XXX123",
            SiteId = Guid.NewGuid(),
        };
        _accountManager
            .CreateAsync(request.PlatformId, request.SiteId, request.InternalId, request.ReferenceValue, request.StartDate, request.EndDate, request.FinYear)
            .Returns<Task<Account>>(x => throw new BussinessException("StartDate cannot be greater than or equal to EndDate."));

        // action
        Func<Task> act = async () => await _sut.Handle(request, CancellationToken.None);

        // assertion
        await act.Should().ThrowAsync<BussinessException>().WithMessage("StartDate cannot be greater than or equal to EndDate.");
    }
}
