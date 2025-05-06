using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using TwitterUalaChallenge.Common.Responses;

namespace TwitterUalaChallenge.Contracts.Core.Api.ExceptionHandler.Base;

public abstract class BaseExceptionHandler<TException> : IExceptionHandler where TException : System.Exception
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        System.Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not TException specificException)
        {
            return false;
        }


        var responseResult = new ApiResponse<object>();
        SetResponse(responseResult, specificException);

        httpContext.Response.StatusCode = SetHttpResponseCode();
        httpContext.Response.ContentType = "application/json";

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(responseResult), cancellationToken);

        return true;
    }

    protected abstract void SetResponse(ApiResponse<object> responseResult, TException exception);

    protected abstract int SetHttpResponseCode();
}