using AsanPardakht.Core.Models.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Security.Authentication;

namespace AsanPardakht.Core.Middlewares;

public class ApiExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ApiExceptionHandler> _logger;

    public ApiExceptionHandler(RequestDelegate next, ILogger<ApiExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        string exceptionMessage;
        byte status;

        if (exception is AuthenticationException)
        {
            context.Response.StatusCode = 401;
            exceptionMessage = "کلید وب سرویس نامعتبر است";
            status = 50;
        }
        else
        {
            context.Response.StatusCode = 500;
            exceptionMessage = "مشکلی در سامانه رخ داده است، لطفا با پشتیبانی در تماس باشید";
            status = 0;
        }

        ApiResponseStructure<string> response = new()
        {
            Data = null,
            Message = exceptionMessage,
            Status = status
        };

        _logger.LogError(exception, $"Request - {context.Request?.Method}: {context.Request?.Path.Value} - " +
            "{ExceptionMessage} - {StatusCode} - {ResponseText}",
                exception.Message, context.Response.StatusCode, response);

        context.Response.ContentType = "application/json";

        return context.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        }));
    }
}