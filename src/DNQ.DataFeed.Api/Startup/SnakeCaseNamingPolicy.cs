using System.Text.Json;

namespace DNQ.DataFeed.Api.Startup;

public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        return string.Concat(name.Select((ch, i) =>
            i > 0 && char.IsUpper(ch) ? "_" + ch.ToString().ToLower() : ch.ToString().ToLower()));
    }
}
