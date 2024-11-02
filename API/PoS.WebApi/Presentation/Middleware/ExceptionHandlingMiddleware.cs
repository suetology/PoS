using System.Net;

namespace PoS.WebApi.Presentation.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IDictionary<Type, HttpStatusCode> _exceptionMappings;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        IDictionary<Type, HttpStatusCode> exceptionMappings)
    {
        _next = next;
        _exceptionMappings = exceptionMappings;
    }
    
    public async Task InvokeAsync(HttpContext context)
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

    private async Task HandleException(HttpContext httpContext, Exception exception)
    {
        var type = exception.GetType();
        var baseType = exception.GetBaseException().GetType();
        
        if (_exceptionMappings?.ContainsKey(type) == true)
        {
            var statusCode = _exceptionMappings[type];
            await HandleResponse(httpContext, exception.Message, statusCode);
        } 
        else if (_exceptionMappings?.ContainsKey(baseType) == true)
        {
            var statusCode = _exceptionMappings[baseType];
            await HandleResponse(httpContext, exception.Message, statusCode);
        }
        else
        {
            var statusCode = HttpStatusCode.InternalServerError;
            await HandleResponse(httpContext, exception.Message, exception.StackTrace, statusCode);
        }
    }

    private static Task HandleResponse(
        HttpContext httpContext,
        string message,
        HttpStatusCode statusCode)
    {
        httpContext.Response.ContentType = "application/text";
        httpContext.Response.StatusCode = (int)statusCode;
        
        return httpContext.Response.WriteAsync($"Error: {message}");
    }
    
    private static Task HandleResponse(
        HttpContext httpContext,
        string message,
        string stackTrace,
        HttpStatusCode statusCode)
    {
        httpContext.Response.ContentType = "application/text";
        httpContext.Response.StatusCode = (int)statusCode;
        
        return httpContext.Response.WriteAsync(
            $"Error: {message}\n" +
            $"StackTrace: {stackTrace}");
    }
}