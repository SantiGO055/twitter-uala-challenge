using TwitterUalaChallenge.Common.Errors;
using TwitterUalaChallenge.Common.Exceptions;
using TwitterUalaChallenge.Common.Responses;

namespace TwitterUalaChallenge.Contracts.Core.Api.ExceptionHandler.Base;

public abstract class CustomExceptionHandler<TCustomException> : BaseExceptionHandler<TCustomException>
    where TCustomException : BaseCustomException
{
    public override ApiResponse<string> Handle(TCustomException exception)
    {
        var apiErrorType = exception.ApiErrorType;

        return ApiResponse<string>.Failure(
            exception.HttpStatusCode,
            new ApiResponseError(
                apiErrorType.ErrorCode.ToString(),
                apiErrorType.ErrorMessage));
    }
}