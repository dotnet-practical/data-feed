namespace DNQ.DataFeed.Application.Accounts.Queries.Dtos;

public class AccountDto
{
    public Guid Id { get; set; }
    public Guid PlatformId { get; set; } 
    public Guid InternalId { get; set; } 
    public string ReferenceValue { get; set; } = default!; 
    public Guid SiteId { get; set; }  
    public DateTime StartDate { get; set; } 
    public DateTime? EndDate { get; set; } 
    public uint FinYear { get; set; } 
}
