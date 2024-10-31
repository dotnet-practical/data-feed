using FluentValidation;

namespace DNQ.DataFeed.Application.Sites.Commands.UpdateSite;

public class UpdateSiteCommandValidator : AbstractValidator<UpdateSiteCommand>
{
    public UpdateSiteCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("Id is invalid.");

        RuleFor(x => x.Code).NotEmpty().NotNull().MaximumLength(50).WithMessage("Code is invalid.");

        RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(100).WithMessage("Name is invalid.");
    }
}
