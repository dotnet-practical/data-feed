namespace DNQ.DataFeed.Application.Accounts.Queries.Dtos;

public class AccountPagingDto
{
    public List<AccountDto> Data { get; init; } = new List<AccountDto>();
    public int? Page { get; init; }
    public int? PageSize { get; init; }
    public int? TotalItems { get; init; }
    public int? TotalPages => (TotalItems.HasValue && PageSize.HasValue && PageSize.Value > 0) ? (int)Math.Ceiling((double)TotalItems.Value / PageSize.Value) : 0;

    public AccountPagingDto(List<AccountDto> data, int totalItems, int? page, int? pageSize)
    {
        Data = data;
        TotalItems = totalItems;
        Page = page;
        PageSize = pageSize;
    }
}
