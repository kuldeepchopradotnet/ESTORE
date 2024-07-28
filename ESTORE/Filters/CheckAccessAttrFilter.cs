using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ESTORE.Filters
{
    public class CheckAccessAttrFilter : ActionFilterAttribute
    {
        public readonly string? _permissions = null;
        public CheckAccessAttrFilter(string permissions)
        {
            _permissions = permissions;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var endpoint = context.HttpContext.GetEndpoint();
            if (endpoint == null)
            {
                return;
            }

            var user = context.HttpContext.User;
            if (user == null || user.Identity == null)
            {
                return;
            }

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userPermissions = user.FindFirst("Permissions")?.Value;
            if (userPermissions == null || !userPermissions.Contains(_permissions ?? ""))
            {
                context.Result = new ForbidResult();
            }

            base.OnActionExecuting(context);    
        }
    }
}
