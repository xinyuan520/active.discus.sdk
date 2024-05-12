using Discus.SDK.Tools.HttpResult;
using Discus.SDK.Tools.HttpResult.Enums;

namespace Discus.Shared.WebApi.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
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
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var requestId = System.Diagnostics.Activity.Current?.Id ?? context.TraceIdentifier;
            var eventId = new EventId(exception.HResult, requestId);
            _logger.LogError(eventId, exception, exception.Message);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            var result = JsonSerializer.Serialize(ApiResult.IsError("服务请求异常！", ApiResultCode.Error));
            await context.Response.WriteAsync(result);
        }
    }
}
