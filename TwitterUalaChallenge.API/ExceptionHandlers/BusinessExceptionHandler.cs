using System.Diagnostics.CodeAnalysis;
using TwitterUalaChallenge.Common.Exceptions;
using TwitterUalaChallenge.Contracts.Core.Api.ExceptionHandler.Base;

namespace TwitterUalaChallenge.API.ExceptionHandlers;

[ExcludeFromCodeCoverage]
public class BusinessExceptionHandler : CustomExceptionHandler<BusinessException>
{
}