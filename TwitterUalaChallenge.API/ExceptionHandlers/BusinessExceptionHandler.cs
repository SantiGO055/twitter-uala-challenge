using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using TwitterUalaChallenge.Common.Exceptions;
using TwitterUalaChallenge.Common.Responses;
using TwitterUalaChallenge.Contracts.Core.Api.ExceptionHandler.Base;

namespace TwitterUalaChallenge.API.ExceptionHandlers;

[ExcludeFromCodeCoverage]
public class BusinessExceptionHandler : BaseExceptionHandler<BusinessException>
{
    protected override void SetResponse(ApiResponse<object> responseResult, BusinessException exception)
    {
        responseResult.Status = "BusinessError";
        responseResult.Message = exception.Message;
    }

    protected override int SetHttpResponseCode()
    {
        return StatusCodes.Status400BadRequest;
    }
}