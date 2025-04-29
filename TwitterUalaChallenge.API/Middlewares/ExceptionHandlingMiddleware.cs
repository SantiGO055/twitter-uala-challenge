using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TwitterUalaChallenge.API.ExceptionHandlers;
using TwitterUalaChallenge.API.Extensions;
using TwitterUalaChallenge.Common.Options;
using TwitterUalaChallenge.Common.Responses;
using TwitterUalaChallenge.Contracts.Core.Api.ExceptionHandler;

namespace TwitterUalaChallenge.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ExceptionHandlingOptions _exceptionHandlingOptions;
    private readonly Dictionary<Type, IBaseExceptionHandler> _exceptionHandlers;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        IOptions<ExceptionHandlingOptions> exceptionHandlingOptions,
        IEnumerable<IBaseExceptionHandler> exceptionHandlers,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _exceptionHandlingOptions = exceptionHandlingOptions.Value;
        _exceptionHandlers = exceptionHandlers
            .ToDictionary(GetGenericExceptionType, handler => handler);
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError("{message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        ApiResponse<string> apiResponse;

        if (_exceptionHandlers.TryGetValue(exception.GetType(), out var handler))
        {
            _logger.LogInformation(
                "Invocando Handler: {handle}, para procesar la excepcion: {exception}",
                handler.GetType().Name,
                exception.GetType().Name);

            apiResponse = handler.Handle(exception);
        }
        else
        {
            _logger.LogInformation(
                "No se encontro handler para procesar la excepcion: {exception}, se invocara: {handler}",
                exception.GetType().Name,
                typeof(DefaultExceptionHandler).Name);

            apiResponse = DefaultExceptionHandler.Handle(
                exception,
                _exceptionHandlingOptions);
        }

        await context.WriteJsonResponseAsync(apiResponse);
    }

    private static Type GetGenericExceptionType(IBaseExceptionHandler handler)
    {
        var interfaceType = handler.GetType().GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IExceptionHandler<>));

        if (interfaceType != null)
        {
            return interfaceType.GetGenericArguments()[0];
        }

        throw new InvalidOperationException(
            $"Handler {handler.GetType().Name} no implementa {typeof(IExceptionHandler<>).Name}.");
    }
}