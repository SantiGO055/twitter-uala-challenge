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


}