using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reservations.Common.Mvc
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(context, exception);
            }
        }

        private static Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var errorContext = new ServiceErrorContext
            {
                Errors = new List<ServiceError>()
            };

            if (exception is ServiceException serviceException)
            {
                context.Response.StatusCode = 400;

                errorContext.Code = "bad_request";
                errorContext.Errors.Add(new ServiceError
                {
                    ErrorMessage = serviceException.Message,
                });
            }
            else
            {
                context.Response.StatusCode = 500;

                errorContext.Code = "internal_error";
                errorContext.Errors.Add(new ServiceError
                {
                    ErrorMessage = exception.Message
                });
            }

            return context.Response.WriteAsync(JsonConvert.SerializeObject(errorContext));
        }
    }
}
