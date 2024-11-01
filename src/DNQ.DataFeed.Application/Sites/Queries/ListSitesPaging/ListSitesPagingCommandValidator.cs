using DNQ.DataFeed.Application.Sites.Queries.GetSite;
using FluentValidation;
using System.Text.RegularExpressions;

namespace DNQ.DataFeed.Application.Sites.Queries.ListSitesPaging;

public class ListSitesPagingCommandValidator : AbstractValidator<ListSitesPagingCommand>
{
    private static readonly string[] AllowedFields = { "code", "name" };
    public ListSitesPagingCommandValidator()
    {
        RuleFor(x => x.Sort)
           .Must(BeAValidSortFormat)
           .WithMessage("Invalid sort format. Use 'field1:asc-field2:desc' and only allowed fields.")
           .When(x => !string.IsNullOrEmpty(x.Sort));

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page is invalid.")
            .When(x => x.Page.HasValue);

        RuleFor(x => x.PageSize)
           .GreaterThanOrEqualTo(1)
           .WithMessage("Page is invalid.")
           .When(x => x.PageSize.HasValue);
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