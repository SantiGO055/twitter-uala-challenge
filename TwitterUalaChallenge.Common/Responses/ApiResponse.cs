using System.Net;
using TwitterUalaChallenge.Common.Errors;

namespace TwitterUalaChallenge.Common.Responses;

public class ApiResponse<T>
{
    public string Status { get; set; }
    public string Message { get; set; }
    public Dictionary<string, string[]> Errors { get; set; }
    public T Data { get; set; }

    public ApiResponse()
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ApiResponse(HttpStatusCode internalServerError, string toString)
    {
        Status = internalServerError.ToString();
        Message = toString;
        Errors = new Dictionary<string, string[]>();
    }
    public bool IsSuccess => Errors == null || Errors.Count == 0;
}