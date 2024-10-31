using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNQ.DataFeed.Application.Sites.Commands.DeleteSite;

public class DeleteSiteCommandValidator : AbstractValidator<DeleteSiteCommand>
{
    public DeleteSiteCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("Id is required");
    }
}
