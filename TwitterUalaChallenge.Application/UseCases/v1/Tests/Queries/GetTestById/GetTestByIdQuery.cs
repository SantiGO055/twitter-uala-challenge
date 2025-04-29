using TwitterUalaChallenge.Contracts.Core.Application;

namespace TwitterUalaChallenge.Application.UseCases.v1.Tests.Queries.GetTestById
{
    public class GetTestByIdQuery(int id) : Request<GetTestByIdResponse>
    {
        public int Id { get; } = id;
        public override bool ExecuteSaveChanges() => false;
    }
}