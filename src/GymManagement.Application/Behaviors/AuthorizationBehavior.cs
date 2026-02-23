using System.Reflection;
using ErrorOr;
using GymManagement.Application.Authorization;
using GymManagement.Application.Common.Interfaces;
using MediatR;

namespace GymManagement.Application.Behaviors;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest: IRequest<TResponse>
    where TResponse: IErrorOr
{
    private readonly ICurrentUserProvider _currentUserProvider;
    
    public AuthorizationBehavior(ICurrentUserProvider currentUserProvider)
    {
        _currentUserProvider = currentUserProvider;
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        IEnumerable<AuthorizeAttribute> attributes = request
            .GetType()
            .GetCustomAttributes<AuthorizeAttribute>()
            .ToList();
        
        if (!attributes.Any())
        {
            return await next(cancellationToken);
        }

        IEnumerable<string> requiredPermissions = attributes
            .SelectMany(attribute => attribute.Permissions?.Split(',') ?? [])
            .ToList();

        IEnumerable<string> userPermissions = _currentUserProvider.GetCurrentUser().Permissions;

        if (requiredPermissions.Except(userPermissions).Count() > 0)
        {
            return (dynamic)Error.Unauthorized();
        }

        return await next(cancellationToken);
    }
}