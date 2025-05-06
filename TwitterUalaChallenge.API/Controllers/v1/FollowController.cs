using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TwitterUalaChallenge.Application.UseCases.v1.Users.Commands.CreateFollow;
using TwitterUalaChallenge.Application.UseCases.v1.Users.Commands.CreateTweet;
using TwitterUalaChallenge.Application.UseCases.v1.Users.Commands.DeleteFollow;
using TwitterUalaChallenge.Common.DTOs;
using TwitterUalaChallenge.Common.Errors;

namespace TwitterUalaChallenge.API.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[ApiController]
public class FollowController(IMediator mediator) : BaseController(mediator)
{
    /// <remarks>
    /// Comentarios:
    ///     - Crea el seguimiento de un usuario a otro
    /// </remarks>
    [SwaggerOperation("Create follow")]
    [SwaggerResponse(200, "Ok")]
    [SwaggerResponse(400, "Bad request", Type = typeof(ApiResponseError))]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] FollowRequest request)
    {
        var command = new CreateFollowCommand(request);
        return await ExecuteRequest<CreateFollowCommand, bool>(command);
    }

    /// <remarks>
    /// Comentarios:
    ///     - Borra el seguimiento de un usuario a otro
    /// </remarks>
    [SwaggerOperation("Delete follow")]
    [SwaggerResponse(200, "Ok")]
    [SwaggerResponse(400, "Bad request", Type = typeof(ApiResponseError))]
    [HttpDelete("{followerId:guid}/{followedId:guid}")]
    public async Task<IActionResult> Delete(Guid followerId, Guid followedId)
    {
        var command = new DeleteFollowCommand { FollowerId = followerId, FollowedId = followedId };
        return await ExecuteRequest<DeleteFollowCommand, bool>(command);
    }
}