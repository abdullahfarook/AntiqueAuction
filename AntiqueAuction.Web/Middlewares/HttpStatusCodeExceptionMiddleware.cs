// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global

using System;
using System.Threading.Tasks;
using AntiqueAuction.Shared.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AntiqueAuction.Web.Middlewares
{
    /// <summary>
    /// Whenever an exception is thrown in application, we can convert this exception into
    /// friendly message which can be sent to client using <see cref="HttpStatusCodeExceptionMiddleware"/>
    /// </summary>
    public class HttpStatusCodeExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<Exception> _logger;

        private const string ServerErrorMessage = "Internal Server Error";
        private const int ServerErrorCode = 500;

        public HttpStatusCodeExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env, ILogger<Exception> logger)
        {
            _next = next;
            _env = env;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleException(httpContext, ex);
            }
        }
        private Task HandleException(HttpContext httpContext, Exception ex) => ex switch
        {

            ValidationException validationException => HandleExceptionAsync(httpContext,
                new UnprocessableException(validationException.Message)),
            AntiqueAuctionException qxException => HandleExceptionAsync(httpContext, qxException),
            _ => HandleExceptionAsync(httpContext, ex)
        };
        private static async Task HandleExceptionAsync(HttpContext context, AntiqueAuctionException exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = exception.Code;
            response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = exception.Message;
            await response.WriteAsync(
                JsonConvert.SerializeObject(
                    new ErrorResponse(exception.GetType().Name.Replace("Exception", "Error"), exception.Code, 
                        exception.Message, context.Request.Headers["CorrelationId"].ToString(), context.TraceIdentifier)));
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "Internal Server Error");
            context.Response.Clear();
            context.Response.StatusCode = ServerErrorCode;
            context.Response.ContentType = "application/json";
            context.Features.Get<IHttpResponseFeature>().ReasonPhrase = ServerErrorMessage;
            await context.Response.WriteAsync(
                JsonConvert.SerializeObject(
                    new ErrorResponse(_env.IsProduction()? ServerErrorMessage : exception.Message, ServerErrorCode, null, context.Request.Headers["CorrelationId"].ToString(), context.TraceIdentifier)));
        }

    }
    public class ErrorResponse
    {
        public ErrorResponse(string message, int code, string description = null, string requestId = null, string traceId = null)
        {
            Message = message;
            Code = code;
            Description = description;
            RequestId = requestId;
            TraceId = traceId;
        }
        public string Message { get; }
        public int Code { get; }
        public string Description { get; }
        public string RequestId { get; }
        public string SessionId { get; }
        public string TraceId { get; }
    }
}
