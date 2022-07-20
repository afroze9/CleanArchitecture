using System.Reflection;
using CleanArchitecture.Application.Common.Abstractions;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Security;
using MediatR;

namespace CleanArchitecture.Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;

    public AuthorizationBehaviour(ICurrentUserService currentUserService, IIdentityService identityService)
    {
        _currentUserService = currentUserService;
        _identityService = identityService;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        List<AuthorizeAttribute> authorizeAttributes =
            request.GetType().GetCustomAttributes<AuthorizeAttribute>().ToList();

        if (authorizeAttributes.Any())
        {
            // Must be authenticated user
            if (string.IsNullOrEmpty(_currentUserService.UserId))
            {
                throw new UnauthorizedAccessException();
            }

            // Role-based authorization
            List<AuthorizeAttribute> authorizeAttributesWithRoles =
                authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles)).ToList();

            if (authorizeAttributesWithRoles.Any())
            {
                bool authorized = false;

                foreach (string[] roles in authorizeAttributesWithRoles.Select(a => a.Roles.Split(',')))
                foreach (string role in roles)
                {
                    bool isInRole = await _identityService.IsInRoleAsync(_currentUserService.UserId, role.Trim());

                    if (isInRole)
                    {
                        authorized = true;
                        break;
                    }
                }

                // Must be a member of at least one role in roles
                if (!authorized)
                {
                    throw new ForbiddenAccessException();
                }
            }

            // Policy-based authorization
            List<AuthorizeAttribute> authorizeAttributesWithPolicies =
                authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy)).ToList();

            if (authorizeAttributesWithPolicies.Any())
            {
                foreach (string policy in authorizeAttributesWithPolicies.Select(a => a.Policy))
                {
                    bool authorized = await _identityService.AuthorizeAsync(_currentUserService.UserId, policy);

                    if (!authorized)
                    {
                        throw new ForbiddenAccessException();
                    }
                }
            }
        }

        // User is authorized / authorization not required
        return await next();
    }
}