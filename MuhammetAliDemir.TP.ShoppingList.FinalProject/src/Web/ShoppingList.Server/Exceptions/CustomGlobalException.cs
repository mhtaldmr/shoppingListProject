﻿using FluentValidation;
using Newtonsoft.Json;
using ShoppingList.Application.ViewModels.Response.BaseResponses;
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

            context.Response.StatusCode = e switch
            {
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                LockRecursionException => (int)HttpStatusCode.Forbidden,
                InvalidOperationException => (int)HttpStatusCode.BadRequest,
                ValidationException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            string message = "\n\n[Error] HTTP " + context.Request.Method + " - " + context.Response.StatusCode + " Error Message : " + e.Message + "\n\n";

            //Nlog file writer service comes here
            _logger.Log(LogLevel.Error, message);

            var result = JsonConvert.SerializeObject(Result.Fail(new { }, e.Message), Formatting.Indented);
            return context.Response.WriteAsync(result);
        }
    }
}