using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GraphQLTodoList.GraphQL
{
    [Route("[controller]")]
    public class GraphQLController : Controller
    {
        private readonly IMediator _mediator;

        public GraphQLController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> GraphQL([FromBody] GraphQL.GQuery query)
        {
            if (query == null) return BadRequest();

            var result = await this._mediator.Send(query);
            
            if (result.Errors?.Count > 0) return BadRequest(result);
            
            return Ok(result);
        }
    }
}
