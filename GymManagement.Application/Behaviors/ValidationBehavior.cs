using ErrorOr;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace GymManagement.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator is null)
            return await next(cancellationToken);

        ValidationResult result = await _validator.ValidateAsync(request, cancellationToken);

        if (result.IsValid)
            return await next(cancellationToken);

        List<Error> errors = result.Errors
            .Select(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage))
            .ToList();

        return (dynamic)errors;
    }
}