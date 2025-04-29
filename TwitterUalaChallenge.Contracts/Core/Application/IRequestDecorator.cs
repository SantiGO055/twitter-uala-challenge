using MediatR;

namespace TwitterUalaChallenge.Contracts.Core.Application;

public interface IRequestDecorator<T> : IRequest<T>
{
    public bool ExecuteSaveChanges();
}