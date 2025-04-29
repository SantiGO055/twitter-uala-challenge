using TwitterUalaChallenge.Common.Responses;

namespace TwitterUalaChallenge.Contracts.Core.Api.ExceptionHandler;

public interface IExceptionHandler<TException> : IBaseExceptionHandler
    where TException : Exception
{
    ApiResponse<string> Handle(TException exception);
}