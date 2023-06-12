using System.Net;
using Gaas.GobbletGobblers.Application;

namespace Gaas.GobbletGobblers.Core.WebApi.MiddleWares
{
    public class ExceptionHandlingMiddleWare : IMiddleware
    {
        private readonly ILogger _logger;

        public ExceptionHandlingMiddleWare(ILogger<ExceptionHandlingMiddleWare> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Caller will either receive 200 (OK) or one of the below;
            try
            {
                await next(context);
                // Catch, handle and log known exceptions below.
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // 500
                                                                                       // When catching general exceptions, we don't want to write them to the response.
                var errorResult = new ErrorResult
                {
                    Message = ex.Message,
                };

                await context.Response.WriteAsJsonAsync(errorResult);
            }

        }
    }
}
