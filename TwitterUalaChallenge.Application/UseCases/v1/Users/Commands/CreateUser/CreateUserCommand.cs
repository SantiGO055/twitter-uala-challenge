using TwitterUalaChallenge.Common.DTOs;
using TwitterUalaChallenge.Contracts.Core.Application;

namespace TwitterUalaChallenge.Application.UseCases.v1.Users.Commands.CreateUser;

public class CreateUserCommand(string userName) : Request<UserResponse>
{
    public string UserName { get; } = userName;
    public override bool ExecuteSaveChanges() => true;
}