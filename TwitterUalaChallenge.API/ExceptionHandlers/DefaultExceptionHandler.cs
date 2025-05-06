using System;
using System.Net;
using TwitterUalaChallenge.Common.Errors;
using TwitterUalaChallenge.Common.Options;
using TwitterUalaChallenge.Common.Responses;

namespace TwitterUalaChallenge.API.ExceptionHandlers;

public static class DefaultExceptionHandler
{
    private static readonly string DEFAULT_STATUS_CODE = "500";

    public static ApiResponse<string> Handle(Exception exception, ExceptionHandlingOptions exceptionHandlingOptions)
    {
        ApiResponseError responseError;

        if (exceptionHandlingOptions.ShowExceptionDetails)
        {
            responseError = new ApiResponseError(
                DEFAULT_STATUS_CODE,
                exception.Message,
                exception.InnerException,
                exception.StackTrace);
        }
        else
        {
            responseError = new ApiResponseError(
                DEFAULT_STATUS_CODE,
                "Ha ocurrido un error inesperado. Por favor, contacte con el soporte tecnico.");
        }

        return new ApiResponse<string>(HttpStatusCode.InternalServerError, responseError.ToString());
    }
}