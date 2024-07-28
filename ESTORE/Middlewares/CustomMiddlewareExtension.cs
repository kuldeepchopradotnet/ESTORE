
namespace ESTORE.Middlewares
{
    public static class CustomMiddlewareExtension
    {
        public static IApplicationBuilder UseLoggin(this IApplicationBuilder builder)
        {
           return builder.UseMiddleware<LogginMiddleware>();
        }


        public static IApplicationBuilder UseExceptions(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
