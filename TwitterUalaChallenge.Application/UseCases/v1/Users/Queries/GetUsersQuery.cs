using TwitterUalaChallenge.Common.DTOs;
using TwitterUalaChallenge.Contracts.Core.Application;

namespace TwitterUalaChallenge.Application.UseCases.v1.Users.Queries;

public class GetUsersQuery : Request<IEnumerable<UserResponse>>
{
    public int Page { get; set; }
    public int Limit { get; set; }
    public override bool ExecuteSaveChanges() => false;
}