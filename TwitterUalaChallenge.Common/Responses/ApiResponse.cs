using System.Net;
using TwitterUalaChallenge.Common.Errors;

namespace TwitterUalaChallenge.Common.Responses;

public class ApiResponse<T>
{
    public T Data { get; private set; }
    public bool IsSuccess { get; private set; }
    public HttpStatusCode HttpStatusCode { get; set; }
    public IList<ApiResponseError> Errors { get; set; }

    protected ApiResponse(
        T value,
        bool isSuccess,
        HttpStatusCode httpStatusCode,
        IList<ApiResponseError> errors = null)
    {
        Data = value;
        IsSuccess = isSuccess;
        HttpStatusCode = httpStatusCode;
        Errors = errors;
    }

    public static ApiResponse<T> Success(HttpStatusCode httpStatusCode, T value)
    {
        return new ApiResponse<T>(value, true, httpStatusCode);
    }

    public static ApiResponse<T> Failure(HttpStatusCode httpStatusCode, ApiResponseError error)
    {
        return new ApiResponse<T>(default, false, httpStatusCode, new List<ApiResponseError> { error });
    }

    public static ApiResponse<T> Failure(HttpStatusCode httpStatusCode, IList<ApiResponseError> errors)
    {
        return new ApiResponse<T>(default, false, httpStatusCode, errors);
    }
}