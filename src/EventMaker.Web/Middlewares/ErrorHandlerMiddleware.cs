using EventMaker.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventMaker.Web.Middlewares
{
    /// <summary>
    /// Global error handler.
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="next">Request delegate.</param>
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Error handler.
        /// </summary>
        /// <param name="context">Http context.</param>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = error switch
                {
                    NotFoundException e => (int)HttpStatusCode.NotFound,// Event not found error
                    EventOwerflowException e => (int)HttpStatusCode.Conflict,//Update conflict error
                    OtherException e => (int)HttpStatusCode.NotFound,// Other error
                    _ => (int)HttpStatusCode.InternalServerError,// Unhandled error
                };

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
