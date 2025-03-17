using MedPoint.Api.Models;
using MedPoint.Service.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace MedPoint.Api.Middlewares
{
    public class ExceptionHandlerMiddleWare(RequestDelegate next, ILogger<ExceptionHandlerMiddleWare> logger)
    {
        private readonly RequestDelegate next = next;
        private readonly ILogger logger = logger;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (MedPointException ex)
            {
                context.Response.StatusCode = ex.StatusCode;
                await context.Response.WriteAsJsonAsync(new Response
                {
                    StatusCode = ex.StatusCode,
                    Message = ex.Message,
                });
            }
            catch (Exception ex)
            {
                this.logger.LogError($"{ex}\n\n");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new Response
                {
                    StatusCode = 500,
                    Message = ex.Message,
                });
            }
        }
    }
}
