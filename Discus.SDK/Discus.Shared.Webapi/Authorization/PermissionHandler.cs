using Microsoft.AspNetCore.Http;
namespace Discus.Shared.WebApi.Authorization
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User.Identity.IsAuthenticated && context.Resource is HttpContext httpContext)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
