using TwitterUalaChallenge.Contracts.Repositories;
using TwitterUalaChallenge.Domain.Entities;
using TwitterUalaChallenge.Application.Services.Interfaces;
using TwitterUalaChallenge.Application.UseCases.v1.Tests.Queries.GetTestById;
using TwitterUalaChallenge.Common.Errors;
using TwitterUalaChallenge.Common.Exceptions;

namespace TwitterUalaChallenge.Application.Services
{
    public class TestService(ITestRepository entityRepository) : ITestService
    {
        private readonly ITestRepository _testRepository = entityRepository;

        public async Task<GetTestByIdResponse> GetTestByIdAsync(int id)
        {
            var entity = await _testRepository.GetSingleAsync(x => x.Id == id);

            return entity != null
                ? (GetTestByIdResponse)entity
                : throw new BusinessException(ApiErrorType.EntityNotExist);
        }

        public async Task<bool> CreateTestAsync(Test test)
        {
            await _testRepository.AddAsync(test);

            return true;
        }
    }
}