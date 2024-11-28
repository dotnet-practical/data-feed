using DNQ.DataFeed.Application.Accounts.Queries.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DNQ.DataFeed.Application.Accounts.Queries.ListAccounts;

public class ListAccountsCommand : IRequest<AccountPagingDto>
{
    public Guid? PlatformId { get; init; }
    public Guid? InternalId { get; init; }
    public string? ReferenceValue { get; init; }
    public Guid? SiteId { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public uint? FinYear { get; init; }
    public string? Sort { get; init; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }

    public void AllocateDefaultValueIfRequired()
    {
        Page = Page.HasValue ? Page.Value : 1;
        PageSize = PageSize.HasValue ? PageSize.Value : 10;
    }
}
