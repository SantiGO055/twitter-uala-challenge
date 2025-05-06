using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TwitterUalaChallenge.Application.UseCases.v1.Timeline;
using TwitterUalaChallenge.Application.UseCases.v1.Timeline.Queries;
using TwitterUalaChallenge.Application.UseCases.v1.Users.Queries;
using TwitterUalaChallenge.Common.DTOs;
using TwitterUalaChallenge.Common.Errors;

namespace TwitterUalaChallenge.API.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[ApiController]
public class TimelineController(IMediator mediator) : BaseController(mediator)
{
    /// <remarks>
    /// Comentarios:
    ///     - Obtiene un usuario
    /// </remarks>
    [SwaggerOperation("Get timeline by user")]
    [SwaggerResponse(200, "Ok", Type = typeof(UserResponse))]
    [SwaggerResponse(404, "Not found", Type = typeof(ApiResponseError))]
    [HttpGet("{userId:guid}")]
    public async Task<PaginatedResponse<TimelineResponse>> Get([FromRoute] Guid userId, [FromQuery] int page = 1, [FromQuery] int limit = 20)
    {
        var request = new GetTimelineByUserQuery(userId, page, limit);
        return await mediator.Send(request);
    }
}