namespace TwitterUalaChallenge.Contracts.Core.Application;

public class Request<T> : IRequestDecorator<T>
{
    public virtual bool ExecuteSaveChanges()
        => false;
}