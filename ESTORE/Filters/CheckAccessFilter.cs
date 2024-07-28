using ESTORE.Attributes;
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
            var actionDescriptor =  context.ActionDescriptor as ControllerActionDescriptor;
            if (actionDescriptor != null)
            {
                var methodAttributes = actionDescriptor.MethodInfo.GetCustomAttributes(typeof(AllowAccessAttribute), false);

                if (methodAttributes.Length > 0)
                {
                    AllowAccessAttribute myAttr = (AllowAccessAttribute)methodAttributes[0];
                    Console.WriteLine("Method Attribute Description: " + myAttr.Access);

                    var user = context.HttpContext.User;
                    var permissions = user.FindFirst("Permissions")?.Value;

                    if(permissions == null || myAttr.Access != permissions)
                    {
                        throw new BadHttpRequestException("Forbidden Access", (int)HttpStatusCode.Forbidden);
                    }
                }
            }
        }
    }
}
