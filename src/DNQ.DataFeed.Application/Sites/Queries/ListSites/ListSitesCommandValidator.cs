using FluentValidation;
using System.Text.RegularExpressions;

namespace DNQ.DataFeed.Application.Sites.Queries.ListSites;

public class ListSitesCommandValidator : AbstractValidator<ListSitesCommand>
{
    private static readonly string[] AllowedFields = { "code" , "name" };

    public ListSitesCommandValidator()
    {
        RuleFor(x => x.Sort)
            .Must(BeAValidSortFormat)
            .WithMessage("Invalid sort format. Use 'field1:asc-field2:desc' and only allowed fields.")
            .When(x => !string.IsNullOrEmpty(x.Sort));
    }

    public bool BeAValidSortFormat(string? sort)
    {
        if (string.IsNullOrEmpty(sort))
        {
            return true;
        }

        // Regex to check the format "field:asc/desc"
        var regex = new Regex(@"^(\w+:(asc|desc)-?)+$", RegexOptions.IgnoreCase);
        if (!regex.IsMatch(sort))
        {
            return false;
        }

        // Validate each field part
        var sortItems = sort.Split('-');
        foreach (var item in sortItems)
        {
            var parts = item.Split(':');
            if (parts.Length != 2 || !AllowedFields.Contains(parts[0]))
            {
                return false; // Invalid field or not in allowed list
            }
        }

        return true;
    }
}
