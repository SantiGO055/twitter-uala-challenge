using MediatR;
using TwitterUalaChallenge.Application.Services.Interfaces;

namespace TwitterUalaChallenge.Application.UseCases.v1.Tests.Queries.GetTestById
{
    public class GetTestByIdHandler(ITestService testService) : IRequestHandler<GetTestByIdQuery, GetTestByIdResponse>
    {
        private readonly ITestService _testService = testService;

        public async Task<GetTestByIdResponse> Handle(GetTestByIdQuery request, CancellationToken cancellationToken)
        {
            return await _testService.GetTestByIdAsync(request.Id);
        }
    }
}