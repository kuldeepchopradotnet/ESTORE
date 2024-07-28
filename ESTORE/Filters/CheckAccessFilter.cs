using ESTORE.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace ESTORE.Filters
{
    public class CheckAccessFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionDescriptor is ControllerActionDescriptor actionDescriptor)
            {
                var myAttr = actionDescriptor.MethodInfo.GetCustomAttributes(typeof(AllowAccessAttribute), false)
                                    .FirstOrDefault() as AllowAccessAttribute;

                if (myAttr != null)
                {
                    var user = context.HttpContext.User;
                    var permissions = user.FindFirst("Permissions")?.Value;

                    if (permissions == null || myAttr.Access != permissions)
                    {
                        context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);
                    }
                }
            }
        }
    }
}
