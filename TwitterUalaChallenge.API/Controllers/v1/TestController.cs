using MediatR;
using TwitterUalaChallenge.Application.UseCases.v1.Tests.Commands.CreateTest;
using TwitterUalaChallenge.Application.UseCases.v1.Tests.Queries.GetTestById;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace TwitterUalaChallenge.API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class TestController(IMediator mediator, ILogger<TestController> logger) : BaseController(mediator)
    {
        private readonly ILogger<TestController> _logger = logger;

        /// <remarks>
        /// Comentarios:
        ///     - retorna una entidad 'Test' dado un Id 
        /// </remarks>
        [SwaggerOperation("Get Test by Id")]
        [SwaggerResponse(200, "Ok", Type = typeof(GetTestByIdResponse))]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation(".");
            var query = new GetTestByIdQuery(id);
            return await ExecuteRequest<GetTestByIdQuery, GetTestByIdResponse>(query);
        }

        /// <remarks>
        /// Comentarios:
        ///     - crea una entidad 'Test'
        /// </remarks>
        [SwaggerOperation("Create Test")]
        [SwaggerResponse(200, "Ok", Type = typeof(bool))]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTestCommand command)
        {
            _logger.LogInformation(".");
            return await ExecuteRequest<CreateTestCommand, bool>(command);
        }
    }
}