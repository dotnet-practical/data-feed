namespace DNQ.DataFeed.Application.Sites.Queries.Dtos;

public class SiteDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
}
