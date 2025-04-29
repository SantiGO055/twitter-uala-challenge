using System.Net;
using TwitterUalaChallenge.Common.Errors;

namespace TwitterUalaChallenge.Common.Exceptions
{
    public class TestException : BaseCustomException
    {
        public override HttpStatusCode HttpStatusCode { get; protected set; } = HttpStatusCode.InternalServerError;

        public TestException(ApiErrorType apiErrorType)
        {
            ApiErrorType = apiErrorType;
        }

        public TestException(ApiErrorType apiErrorType, HttpRequestException ex)
            : base(ex.Message, ex)
        {
            ApiErrorType = apiErrorType;
            HttpStatusCode = ex.StatusCode ?? HttpStatusCode;
        }

        public TestException(ApiErrorType apiErrorType, HttpStatusCode httpStatusCode)
        {
            ApiErrorType = apiErrorType;
            HttpStatusCode = httpStatusCode;
        }
    }
}