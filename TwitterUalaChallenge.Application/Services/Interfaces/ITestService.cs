using TwitterUalaChallenge.Domain.Entities;
using TwitterUalaChallenge.Application.UseCases.v1.Tests.Queries.GetTestById;

namespace TwitterUalaChallenge.Application.Services.Interfaces
{
    public interface ITestService
    {
        Task<GetTestByIdResponse> GetTestByIdAsync(int id);
        Task<bool> CreateTestAsync(Test test);
    }
}