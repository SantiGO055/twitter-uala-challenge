using Microsoft.AspNetCore.Http;
using TwitterUalaChallenge.Common.Errors;
using TwitterUalaChallenge.Common.Exceptions;
using TwitterUalaChallenge.Common.Responses;

namespace TwitterUalaChallenge.Contracts.Core.Api.ExceptionHandler.Base;

public abstract class CustomExceptionHandler<TCustomException> : BaseExceptionHandler<System.Exception>
{
    protected override void SetResponse(ApiResponse<object> responseResult, System.Exception exception)
    {
        responseResult.Status = "ValidationError";
        responseResult.Message = "Ocurrió un error de validación";
        responseResult.Errors = new Dictionary<string, string[]>
        {
            { "Error", [exception.Message] }
        };
    }
    protected override int SetHttpResponseCode()
    {
        return StatusCodes.Status500InternalServerError;
    }
}