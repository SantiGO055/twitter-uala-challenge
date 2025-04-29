using TwitterUalaChallenge.Common.Responses;

namespace TwitterUalaChallenge.Contracts.Core.Api.ExceptionHandler;

public interface IBaseExceptionHandler
{
    ApiResponse<string> Handle(Exception exception);
}