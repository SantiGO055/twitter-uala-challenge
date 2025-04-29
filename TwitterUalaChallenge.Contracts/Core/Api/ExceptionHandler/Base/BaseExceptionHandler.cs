using TwitterUalaChallenge.Common.Responses;

namespace TwitterUalaChallenge.Contracts.Core.Api.ExceptionHandler.Base;

public abstract class BaseExceptionHandler<TException> : IExceptionHandler<TException>
    where TException : Exception
{
    public abstract ApiResponse<string> Handle(TException exception);

    public ApiResponse<string> Handle(Exception exception)
    {
        return Handle((TException)exception);
    }
}