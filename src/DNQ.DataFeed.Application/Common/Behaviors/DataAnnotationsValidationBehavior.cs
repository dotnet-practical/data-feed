using MediatR;
using System.ComponentModel.DataAnnotations;

namespace DNQ.DataFeed.Application.Common.Behaviors;

public class DataAnnotationsValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext(request, null, null);
        Validator.ValidateObject(request, context, validateAllProperties: true);

        return next();
    }
}
