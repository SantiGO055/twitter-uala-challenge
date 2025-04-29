using TwitterUalaChallenge.Domain.Entities;

namespace TwitterUalaChallenge.Application.UseCases.v1.Tests.Queries.GetTestById
{
    public class GetTestByIdResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public static explicit operator GetTestByIdResponse(Test @object)
        {
            if (@object == null)
                return null;

            return new GetTestByIdResponse
            {
                Id = @object.Id,
                Description = @object.Description,
            };
        }
    }
}