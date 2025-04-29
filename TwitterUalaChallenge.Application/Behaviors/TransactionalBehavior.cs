using MediatR;
using Microsoft.Extensions.Logging;
using TwitterUalaChallenge.Contracts.Core.Application;
using TwitterUalaChallenge.Contracts.Core.Infraestructure;

namespace TwitterUalaChallenge.Application.Behaviors;

public class TransactionalBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequestDecorator<TResponse>

{
    private readonly ILogger<TransactionalBehavior<TRequest, TResponse>> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public TransactionalBehavior(ILogger<TransactionalBehavior<TRequest, TResponse>> logger, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _logger.LogInformation($"{nameof(TransactionalBehavior<TRequest, TResponse>)} ha sido registrado.");
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        if (request.ExecuteSaveChanges())
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        return response;
    }
}