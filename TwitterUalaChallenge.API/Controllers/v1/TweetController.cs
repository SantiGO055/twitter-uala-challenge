using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TwitterUalaChallenge.Application.UseCases.v1.Tweet.Commands.CreateTweet;
using TwitterUalaChallenge.Common.DTOs;
using TwitterUalaChallenge.Common.Errors;

namespace TwitterUalaChallenge.API.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[ApiController]
public class TweetController(IMediator mediator) : BaseController(mediator)
{
    /// <remarks>
    /// Comentarios:
    ///     - Crea un usuario retornando el id del usuario creado
    /// </remarks>
    [SwaggerOperation("Create tweet")]
    [SwaggerResponse(200, "Ok", Type = typeof(TweetResponse))]
    [SwaggerResponse(400, "Bad request", Type = typeof(ApiResponseError))]
    [HttpPost]
    public async Task<TweetResponse> Create([FromBody] CreateTweetCommand request)
    {
        return await mediator.Send(request);
    }
}