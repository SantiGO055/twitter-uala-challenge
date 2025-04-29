using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TwitterUalaChallenge.Common.Responses;
using TwitterUalaChallenge.Contracts.Core.Application;

namespace TwitterUalaChallenge.API.Controllers;

public class BaseController(IMediator mediator) : Controller
{
    protected readonly IMediator _mediator = mediator;

    protected async Task<IActionResult> ExecuteRequest<TRequest, TResponse>(TRequest request)
        where TRequest : Request<TResponse>
    {
        var response = await _mediator.Send(request);

        var apiResponse = ApiResponse<TResponse>.Success(HttpStatusCode.OK, response);
        return Ok(apiResponse);
    }
}