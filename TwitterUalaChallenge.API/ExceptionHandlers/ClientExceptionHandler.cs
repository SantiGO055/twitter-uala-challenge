using Microsoft.AspNetCore.Http;
using TwitterUalaChallenge.Common.Exceptions;
using TwitterUalaChallenge.Common.Responses;
using TwitterUalaChallenge.Contracts.Core.Api.ExceptionHandler.Base;

namespace TwitterUalaChallenge.API.ExceptionHandlers;

public class ClientExceptionHandler : BaseExceptionHandler<ClientException>
{
    protected override void SetResponse(ApiResponse<object> responseResult, ClientException exception)
    {
        responseResult.Status = "ValidationError";
        responseResult.Message = "Ocurrió un error de validación";
    }

    protected override int SetHttpResponseCode()
    {
        return StatusCodes.Status500InternalServerError;
    }
}