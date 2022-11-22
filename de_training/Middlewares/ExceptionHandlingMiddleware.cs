using de_training.Exceptions;
using System.Net;

namespace de_training.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpException httpException)
            {
                context.Response.StatusCode = (int)httpException.StatusCode;
                await context.Response.WriteAsync(httpException.Message);
                return;
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync("Unknown error has occured");
                return;
            }
        }
    }
}
