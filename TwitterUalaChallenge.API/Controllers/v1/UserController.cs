using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TwitterUalaChallenge.Application.UseCases.v1.Follow.Commands.CreateUser;
using TwitterUalaChallenge.Application.UseCases.v1.Users.Queries;
using TwitterUalaChallenge.Common.DTOs;
using TwitterUalaChallenge.Common.Errors;

namespace TwitterUalaChallenge.API.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[ApiController]
public class UserController(IMediator mediator) : BaseController(mediator)
{
    /// <remarks>
    /// Comentarios:
    ///     - Crea un usuario retornando el id del usuario creado
    /// </remarks>
    [SwaggerOperation("Create user")]
    [SwaggerResponse(200, "Ok", Type = typeof(UserResponse))]
    [SwaggerResponse(400, "Bad request", Type = typeof(ApiResponseError))]
    [HttpPost]
    public async Task<UserResponse> Create([FromBody] CreateUserCommand request)
    {
        return await mediator.Send(request);
    }

    /// <remarks>
    /// Comentarios:
    ///     - Obtiene un usuario
    /// </remarks>
    [SwaggerOperation("Get User by Id")]
    [SwaggerResponse(200, "Ok", Type = typeof(UserResponse))]
    [SwaggerResponse(404, "Not found", Type = typeof(ApiResponseError))]
    [HttpGet("{id:guid}")]
    public async Task<UserResponse> Get(Guid id)
    {
        var request = new GetUserByIdQuery(id);
        return await mediator.Send(request);
    }

    /// <remarks>
    /// Comentarios:
    ///     - Obtiene un listado de usuarios
    /// </remarks>
    [SwaggerOperation("Get list of Users")]
    [SwaggerResponse(200, "Ok", Type = typeof(IEnumerable<UserResponse>))]
    [SwaggerResponse(404, "Not found", Type = typeof(ApiResponseError))]
    [HttpGet]
    public async Task<IEnumerable<UserResponse>> GetAll([FromQuery] int page = 1, [FromQuery] int limit = 10)
    {
        var request = new GetUsersQuery { Page = page, Limit = limit };
        return await mediator.Send(request);
    }
}