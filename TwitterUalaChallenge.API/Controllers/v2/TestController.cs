using MediatR;
using TwitterUalaChallenge.Application.UseCases.v1.Tests.Queries.GetTestById;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace TwitterUalaChallenge.API.Controllers.v2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2")]
    [ApiController]
    public class TestController(IMediator mediator) : BaseController(mediator)
    {
        /// <remarks>
        /// Comentarios:
        ///     - retorna una entidad dado un Id 
        /// </remarks>
        [SwaggerOperation("Get entity by Id")]
        [SwaggerResponse(200, "Ok", Type = typeof(GetTestByIdResponse))]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetTestByIdQuery(id);
            return await ExecuteRequest<GetTestByIdQuery, GetTestByIdResponse>(query);
        }
    }
}