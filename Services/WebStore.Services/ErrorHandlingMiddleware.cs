using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebStore.Services
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(ErrorHandlingMiddleware));

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
                throw;
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            _log.Error(exception.Message, exception);
            return Task.CompletedTask;
        }
    }
}
