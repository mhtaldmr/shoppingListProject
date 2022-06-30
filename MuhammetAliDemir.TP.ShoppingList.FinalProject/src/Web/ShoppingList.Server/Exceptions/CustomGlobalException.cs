using Newtonsoft.Json;
using System.Net;

namespace ShoppingList.Server.Exceptions
{
    public class CustomGlobalException
    {
        private readonly RequestDelegate _next;
        public CustomGlobalException(RequestDelegate next) => _next = next;

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

            string message = "[Error] HTTP " + context.Request.Method + " - " + context.Response.StatusCode + " Error Message : " + e.Message;
            
            //Nlog file writer service will come here

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