using MediatR;
using TwitterUalaChallenge.Application.Services.Interfaces;
using TwitterUalaChallenge.Common.DTOs;

namespace TwitterUalaChallenge.Application.UseCases.v1.Users.Commands.CreateUser;

public class CreateUserHandler(IUserService userService) : IRequestHandler<CreateUserCommand, UserResponse>
{
    public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var response = await userService.CreateUserAsync(request.UserName);

        return new UserResponse
        {
            UserId = response.UserId,
            UserName = response.UserName,
            CreatedDate = response.CreatedDate
        };
    }
}