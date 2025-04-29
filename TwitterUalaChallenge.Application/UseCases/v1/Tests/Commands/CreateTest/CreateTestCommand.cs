using TwitterUalaChallenge.Contracts.Core.Application;

namespace TwitterUalaChallenge.Application.UseCases.v1.Tests.Commands.CreateTest
{
    public class CreateTestCommand(string description) : Request<bool>
    {
        public string Description { get; } = description;
        public override bool ExecuteSaveChanges() => true;
    }
}