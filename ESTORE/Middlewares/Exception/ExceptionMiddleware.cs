using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace ESTORE.Middlewares
{
    public class ExceptionMiddleware
    {
        RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
                _next = next;   
        }

        //synchronous 
       /* public void Invoke(HttpContent context)
        {
       _next(context);
        }*/

        //asynchronous
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {

                await HandleExceptionAysync(context, exception);
            }
        }

        private static Task HandleExceptionAysync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            var result = new
            {
                message = "An unexcepted error occoured, Try again",
                detailed = exception.Message
            };
            var jsonStrResult = JsonSerializer.Serialize(result);
            return context.Response.WriteAsync(jsonStrResult);
        }

    }
}
