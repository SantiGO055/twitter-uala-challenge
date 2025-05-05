using MediatR;
using TwitterUalaChallenge.Application.Services.Interfaces;
using TwitterUalaChallenge.Common.DTOs;

namespace TwitterUalaChallenge.Application.UseCases.v1.Users.Queries;

public class GetUsersHandler(IUserService userService) : IRequestHandler<GetUsersQuery, IEnumerable<UserResponse>>
{
    public async Task<IEnumerable<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userService.GetUsersAsync(request.Page, request.Limit);
        
        return users.Select(user => new UserResponse
        {
            UserId = user.UserId,
            UserName = user.UserName,
            CreatedDate = user.CreatedDate
        });
    }
}