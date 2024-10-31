using FluentValidation;

namespace DNQ.DataFeed.Application.Sites.Queries.GetSite;

public class GetSiteCommandValidator : AbstractValidator<GetSiteCommand>
{
    public GetSiteCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("Id is invalid.");
    }
}
