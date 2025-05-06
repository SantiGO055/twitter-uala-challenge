using System.Net;
using FluentValidation.Results;

namespace TwitterUalaChallenge.Common.Exceptions;

public class ValidationCustomException : Exception
{
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.BadRequest;
    public IDictionary<string, string[]> Errors { get; }

    public ValidationCustomException() : base("Uno o más errores de validación han ocurrido")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationCustomException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
}