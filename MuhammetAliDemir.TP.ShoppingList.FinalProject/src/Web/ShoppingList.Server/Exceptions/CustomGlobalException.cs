using Newtonsoft.Json;
using System.Net;

namespace ShoppingList.Server.Exceptions
{
    public class CustomGlobalException
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public CustomGlobalException(RequestDelegate next, ILogger<CustomGlobalException> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleException(context, e);
            }
        }

        private Task HandleException(HttpContext context, Exception e)
        {
            context.Response.ContentType = "application/json";

            string message = "\n\n[Error] HTTP " + context.Request.Method + " - " + context.Response.StatusCode + " Error Message : " + e.Message +"\n \n";
            
            //Nlog file writer service comes here
            _logger.Log(LogLevel.Error, message);

            context.Response.StatusCode = e switch
            {
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                LockRecursionException => (int)HttpStatusCode.Forbidden,
                InvalidOperationException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };
            var result = JsonConvert.SerializeObject(new { error = e.Message }, Formatting.Indented);
            return context.Response.WriteAsync(result);
        }
    }
}