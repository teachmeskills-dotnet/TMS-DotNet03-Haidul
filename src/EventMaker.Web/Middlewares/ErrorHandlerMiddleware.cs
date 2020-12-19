using System;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EventMaker.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

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
            context = context ?? throw new ArgumentNullException(nameof(context));

            try
            {
                await _next(context);
            }
            catch (OtherException<IdentityError> error)
            {
                var response = context.Response;
                var errorList = new StringBuilder();
                response.ContentType = "text/HTML";

                foreach (var err in error.ErrorCollection)
                {
                    errorList.Append($"{err.Description} ");
                }
                await response.WriteAsync(errorList.ToString());
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "text/HTML";

                response.StatusCode = error switch
                {
                    NotFoundException e => (int)HttpStatusCode.NotFound,// Event not found error
                    EventOwerflowException e => (int)HttpStatusCode.Conflict,//Update conflict error
                    OtherException<IdentityError> e => (int)HttpStatusCode.NotFound,// Other error
                    OtherException<string> e => (int)HttpStatusCode.NotFound,// Other error
                    _ => (int)HttpStatusCode.InternalServerError,// Unhandled error
                };

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync($"Error : {response.StatusCode} - {error.Message}");

            }

        }
    }
}
