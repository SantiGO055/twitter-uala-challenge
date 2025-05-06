using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TwitterUalaChallenge.Common.Responses;

namespace TwitterUalaChallenge.API.Extensions;

public static class HttpContextExtensions
{
    public static async Task WriteJsonResponseAsync<T>(this HttpContext context, ApiResponse<T> apiResponse) where T : class
    {
        var response = context.Response;
        response.ContentType = "application/json";


        string jsonResponse;

        if (apiResponse.IsSuccess)
        {
            jsonResponse = JsonSerializer.Serialize(apiResponse.Data);
        }
        else
        {
            jsonResponse = JsonSerializer.Serialize(apiResponse.Errors);
        }

        await context.Response.WriteAsync(jsonResponse);
    }
}