using DNQ.DataFeed.Application.Sites.Commands.CreateSite;
using DNQ.DataFeed.Domain.Common.Exceptions;
using DNQ.DataFeed.Domain.Common.Interfaces;
using DNQ.DataFeed.Domain.Sites;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace DNQ.DataFeed.Application.Tests.Unit.Sites;

public class CreateSiteCommandHandlerTests
{
    private readonly ISiteRepo _siteRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISiteManager _siteManager;
    private readonly CreateSiteCommandHandler _sut;
    public CreateSiteCommandHandlerTests() 
    {
        _siteRepo = Substitute.For<ISiteRepo>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _siteManager = Substitute.For<ISiteManager>();

        _sut = new CreateSiteCommandHandler(_siteRepo, _siteManager, _unitOfWork);
    }

    [Fact]
    public async Task Handle_WithCorrectCommand_ReturnSiteGuid()
    {
        /* Arrange */
        var command = new CreateSiteCommand { Code = "TEST", Name = "Test" };
        var createdSite = new Site(command.Code, command.Name);
        _siteManager.CreateAsync(command.Code, command.Name).Returns(Task.FromResult(createdSite));

        _siteRepo.AddSite(createdSite).Returns(Task.CompletedTask);
        _unitOfWork.CommitChangesAsync().Returns(Task.CompletedTask);

        /* Act */
        var result = await _sut.Handle(command, CancellationToken.None);

        /* Assert */
        result.Should().Be(createdSite.Id);
    }

    [Fact]
    public async Task Handle_WithCreateSiteManagerException_ThrowException()
    {
        /* Arrange */
        var command = new CreateSiteCommand { Code = "TEST", Name = "Test" };
        var createdSite = new Site(command.Code, command.Name);
        var errorMessage = "Cannot have two sites with the same code.";

        _siteManager.CreateAsync(command.Code, command.Name).ThrowsAsync(new BussinessException(errorMessage));
        _siteRepo.AddSite(createdSite).Returns(Task.CompletedTask);
        _unitOfWork.CommitChangesAsync().Returns(Task.CompletedTask);

        /* Act */
        Func<Task> result = async () => await _sut.Handle(command, CancellationToken.None);

        /* Assert */
        await result.Should().ThrowAsync<BussinessException>().WithMessage(errorMessage);
    }

    [Fact]
    public async Task Handle_WithSiteRepoException_ThrowException()
    {
        /* Arrange */
        var command = new CreateSiteCommand { Code = "TEST", Name = "Test" };
        var createdSite = new Site(command.Code, command.Name);
        var errorMessage = "Error when repo add site.";

        _siteManager.CreateAsync(command.Code, command.Name).Returns(Task.FromResult(createdSite));
        _siteRepo.AddSite(createdSite).ThrowsAsync(new BussinessException(errorMessage));
        _unitOfWork.CommitChangesAsync().Returns(Task.CompletedTask);

        /* Act */
        Func<Task> result = async () => await _sut.Handle(command, CancellationToken.None);

        /* Assert */
        await result.Should().ThrowAsync<BussinessException>().WithMessage(errorMessage);
    }

    [Fact]
    public async Task Handle_WithUnitOfWorkException_ThrowException()
    {
        /* Arrange */
        var command = new CreateSiteCommand { Code = "TEST", Name = "Test" };
        var createdSite = new Site(command.Code, command.Name);
        var errorMessage = "Error when repo add site.";

        _siteManager.CreateAsync(command.Code, command.Name).Returns(Task.FromResult(createdSite));
        _siteRepo.AddSite(createdSite).Returns(Task.CompletedTask); 
        _unitOfWork.CommitChangesAsync().ThrowsAsync(new BussinessException(errorMessage));

        /* Act */
        Func<Task> result = async () => await _sut.Handle(command, CancellationToken.None);

        /* Assert */
        await result.Should().ThrowAsync<BussinessException>().WithMessage(errorMessage);
    }
}
