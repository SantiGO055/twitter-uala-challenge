using TwitterUalaChallenge.Common.DTOs;
using TwitterUalaChallenge.Contracts.Core.Application;

namespace TwitterUalaChallenge.Application.UseCases.v1.Users.Queries;

public class GetUserByIdQuery(Guid id) : Request<UserResponse>
{
    public Guid Id { get; } = id;
    public override bool ExecuteSaveChanges() => false;
}