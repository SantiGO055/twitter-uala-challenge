using MediatR;
using TwitterUalaChallenge.Domain.Entities;
using TwitterUalaChallenge.Application.Services.Interfaces;

namespace TwitterUalaChallenge.Application.UseCases.v1.Tests.Commands.CreateTest
{
    public class CreateTestHandler(ITestService testService) : IRequestHandler<CreateTestCommand, bool>
    {
        private readonly ITestService _testService = testService;

        public async Task<bool> Handle(CreateTestCommand request, CancellationToken cancellationToken)
        {
            var test = new Test
            {
                Description = request.Description
            };

            return await _testService.CreateTestAsync(test);
        }
    }
}