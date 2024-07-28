using ESTORE.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ESTORE.Filters
{
    public class CheckAccessAuthFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            var endpoint = context.HttpContext.GetEndpoint();
            if(endpoint == null)
            {
                return;
            }

            var allowAccessAttr = endpoint.Metadata.GetMetadata<AllowAccessAttribute>();
            if (allowAccessAttr == null)
            {
                return;
            }

            var user = context.HttpContext.User;
            if(user == null || user.Identity == null)
            {
                return;
            }

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var permissions = user.FindFirst("Permissions")?.Value;
            if (permissions == null || !permissions.Contains(allowAccessAttr.Access ?? "")){
                context.Result = new ForbidResult();
            }
        }
    }
}
