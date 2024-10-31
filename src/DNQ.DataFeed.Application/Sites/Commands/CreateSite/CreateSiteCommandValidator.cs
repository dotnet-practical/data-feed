using FluentValidation;

namespace DNQ.DataFeed.Application.Sites.Commands.CreateSite;

public class CreateSiteCommandValidator : AbstractValidator<CreateSiteCommand>
{
    public CreateSiteCommandValidator()
    {
        RuleFor(x => x.Code).NotEmpty().NotNull().MaximumLength(50).WithMessage("Code is invalid.");

        RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(100).WithMessage("Name is invalid.");
    }
}
