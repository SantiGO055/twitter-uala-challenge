using System.Net;
using TwitterUalaChallenge.Common.Errors;

namespace TwitterUalaChallenge.Common.Exceptions;

public class BusinessException : BaseCustomException
{
    public override HttpStatusCode HttpStatusCode { get; protected set; } = HttpStatusCode.InternalServerError;

    public BusinessException(ApiErrorType apiErrorType)
    {
        ApiErrorType = apiErrorType;
    }

    public BusinessException(ApiErrorType apiErrorType, HttpRequestException ex)
        : base(ex.Message, ex)
    {
        ApiErrorType = apiErrorType;
        HttpStatusCode = ex.StatusCode ?? HttpStatusCode;
    }

    public BusinessException(ApiErrorType apiErrorType, HttpStatusCode httpStatusCode)
    {
        ApiErrorType = apiErrorType;
        HttpStatusCode = httpStatusCode;
    }
}