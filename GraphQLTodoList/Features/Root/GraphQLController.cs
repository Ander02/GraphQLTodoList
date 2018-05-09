using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.Features.Root
{
    public class GraphQLController : Controller
    {
        private readonly IMediator _mediator;

        public GraphQLController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> GraphQL([FromBody] GraphQL.GraphQuery graphQuery)
        {
            var result = await _mediator.Send(graphQuery);

            if (result.Errors.Any()) return BadRequest(result);

            return Ok(result);
        }
    }
}
