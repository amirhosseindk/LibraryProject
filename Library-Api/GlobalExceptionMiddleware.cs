using log4net;

namespace Library_Api
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                ILog log = LogManager.GetLogger(typeof(GlobalExceptionMiddleware));
                log.Error("An unhandled exception occurred.", ex);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("Internal Server Error");
            }
        }
    }
}