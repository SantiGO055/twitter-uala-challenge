using System.Text.Json.Serialization;

namespace TwitterUalaChallenge.Common.Errors;

public class ApiResponseError
{
    public ApiResponseError(
        string errorCode,
        string errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }

    public ApiResponseError(
        string errorCode,
        string errorMessage,
        Exception innerException,
        string stackTrace)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
        InnerException = innerException;
        StackTrace = stackTrace;
    }

    public string ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
    [JsonIgnore]
    public Exception InnerException { get; }
    public string StackTrace { get; }
}