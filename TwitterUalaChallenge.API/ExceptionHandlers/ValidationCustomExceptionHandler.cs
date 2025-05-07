using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwitterUalaChallenge.Common.Errors;
using TwitterUalaChallenge.Common.Exceptions;
using TwitterUalaChallenge.Common.Responses;
using TwitterUalaChallenge.Contracts.Core.Api.ExceptionHandler;
using TwitterUalaChallenge.Contracts.Core.Api.ExceptionHandler.Base;

namespace TwitterUalaChallenge.API.ExceptionHandlers;

public class ValidationCustomExceptionHandler: BaseExceptionHandler<FluentValidation.ValidationException>, IExceptionHandler
{
    protected override void SetResponse(ApiResponse<object> responseResult, ValidationException exception)
    {
        responseResult.Status = "ValidationError";
        responseResult.Message = "Ocurrió un error de validación";
        responseResult.Errors = exception.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToArray()
            );
    }

    protected override int SetHttpResponseCode()
    {
        return StatusCodes.Status400BadRequest;
    }
}