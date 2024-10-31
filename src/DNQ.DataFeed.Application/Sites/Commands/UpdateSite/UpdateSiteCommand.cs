using MediatR;
using System.Text.Json.Serialization;

namespace DNQ.DataFeed.Application.Sites.Commands.UpdateSite
{
    public class UpdateSiteCommand : IRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Code { get; init; } = null!;
        public string Name { get; init; } = null!;
    }
}
