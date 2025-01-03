﻿using System.Net;
using PoS.WebApi.Presentation.Middleware;

namespace PoS.WebApi.Presentation.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandling(
        this IApplicationBuilder app,
        IDictionary<Type, HttpStatusCode> exceptionMappings)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>(exceptionMappings);
    }
}