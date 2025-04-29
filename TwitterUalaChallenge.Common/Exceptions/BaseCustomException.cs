using System.Net;
using TwitterUalaChallenge.Common.Errors;

namespace TwitterUalaChallenge.Common.Exceptions;

public abstract class BaseCustomException : Exception
{
    public virtual HttpStatusCode HttpStatusCode { get; protected set; } = HttpStatusCode.InternalServerError;
    public ApiErrorType ApiErrorType { get; set; }
    public List<ApiResponseError> Errors { get; set; }

    public BaseCustomException()
    {
    }

    public BaseCustomException(string message)
        : base(message)
    {
    }

    public BaseCustomException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}