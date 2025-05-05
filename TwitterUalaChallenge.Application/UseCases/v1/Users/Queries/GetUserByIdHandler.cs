using MediatR;
using TwitterUalaChallenge.Application.Services.Interfaces;
using TwitterUalaChallenge.Common.DTOs;

namespace TwitterUalaChallenge.Application.UseCases.v1.Users.Queries;

public class GetUserByIdHandler(IUserService userService) : IRequestHandler<GetUserByIdQuery, UserResponse>
{
    public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await userService.GetUserByIdAsync(request.Id);

        return new UserResponse
        {
            UserId = response.UserId,
            UserName = response.UserName,
            CreatedDate = response.CreatedDate
        };
    }
}