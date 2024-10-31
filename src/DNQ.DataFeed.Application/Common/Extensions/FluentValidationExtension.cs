using FluentValidation;
using FluentValidation.Results;

namespace DNQ.DataFeed.Application.Common.Extensions;

public class FluentValidationExtension
{
    public static ValidationException CreateException(string message)
    {
        return new ValidationException(new[]{ new ValidationFailure("", message) });
    }
}
