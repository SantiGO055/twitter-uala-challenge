using System.Net;
using TwitterUalaChallenge.Common.Errors;

namespace TwitterUalaChallenge.Common.Exceptions;

public class ClientException : BaseCustomException
{
    public override HttpStatusCode HttpStatusCode { get; protected set; } = HttpStatusCode.InternalServerError;

    public ClientException(ApiErrorType apiErrorType)
    {
        ApiErrorType = apiErrorType;
    }

    public ClientException(ApiErrorType apiErrorType, HttpRequestException ex)
        : base(ex.Message, ex)
    {
        ApiErrorType = apiErrorType;
        HttpStatusCode = ex.StatusCode ?? HttpStatusCode;
    }

    public ClientException(ApiErrorType apiErrorType, HttpStatusCode httpStatusCode)
    {
        ApiErrorType = apiErrorType;
        HttpStatusCode = httpStatusCode;
    }
}