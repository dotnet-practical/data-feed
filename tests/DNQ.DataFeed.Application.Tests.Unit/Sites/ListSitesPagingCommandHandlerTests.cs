using DNQ.DataFeed.Application.Sites.Queries.ListSitesPaging;
using DNQ.DataFeed.Domain.Common.Interfaces;
using DNQ.DataFeed.Domain.Sites;
using FluentAssertions;
using NSubstitute;
using System.Linq.Expressions;

namespace DNQ.DataFeed.Application.Tests.Unit.Sites;

public class ListSitesPagingCommandHandlerTests
{
    private readonly ISiteRepo _siteRepo;
    private readonly ListSitesPagingCommandHandler _sut;
    public ListSitesPagingCommandHandlerTests() 
    {
        _siteRepo = Substitute.For<ISiteRepo>();
        _sut = new ListSitesPagingCommandHandler(_siteRepo);
    }

    [Fact]
    public async Task Handle_WithCodeNameAreEmpty_ShouldReturnAllSites()
    {
        // arrange
        var command = new ListSitesPagingCommand() { Code = "", Name = "", Page = 1, PageSize = 10, Sort = "code:asc" };
        var sites = new List<Site>() 
        { 
            new Site("TEST01", "Test01"), 
            new Site("TEST02", "Test02"),
            new Site("TEST03", "Test03"),
            new Site("TEST04", "Test04"),
        };
        var totalItems = 4;
        _siteRepo
            .ListAsync(Arg.Any<Expression<Func<Site, bool>>>(), command.Sort, command.Page, command.PageSize)
            .Returns(sites);
        _siteRepo
            .CountAsync(Arg.Any<Expression<Func<Site, bool>>>())
            .Returns(totalItems);

        // action 
        var result = await _sut.Handle(command, CancellationToken.None);

        // assert
        result.Should().NotBeNull();
        result.Data.Should().HaveCount(4);
        result.TotalItems.Should().Be(totalItems);
        result.Page.Should().Be(command.Page.Value);
        result.PageSize.Should().Be(command.PageSize.Value);
    }
}
