using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using TwitterUalaChallenge.Common.Exceptions;

namespace TwitterUalaChallenge.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

    public ValidationBehavior(
        IEnumerable<IValidator<TRequest>> validators,
        ILogger<ValidationBehavior<TRequest, TResponse>> logger)
    {
        _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _logger.LogInformation($"{nameof(ValidationBehavior<TRequest, TResponse>)} ha sido registrado.");
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults =
                await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var errors = validationResults
                .SelectMany(result => result.Errors)
                .Where(failure => failure != null)
                .ToList();

            if (errors.Any())
                throw new ValidationException(errors);
        }

        return await next();
    }
}