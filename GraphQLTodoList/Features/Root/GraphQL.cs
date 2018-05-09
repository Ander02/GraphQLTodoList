using FluentValidation;
using GraphQL;
using GraphQL.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.Features.Root
{
    public class GraphQL
    {
        public class GraphQuery : IRequest<ExecutionResult>
        {
            public string OperationName { get; set; }
            public string NamedQuery { get; set; }
            public string Query { get; set; }
            public string Variables { get; set; }

            public override string ToString() => Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public class CommandValidator : AbstractValidator<GraphQuery>
        {
            public CommandValidator()
            {
                //Validations               
            }
        }

        public class Handler : AsyncRequestHandler<GraphQuery, ExecutionResult>
        {
            private readonly ISchema schema;
            private readonly IDocumentExecuter documentExecuter;

            public Handler(ISchema schema, IDocumentExecuter documentExecuter)
            {
                this.schema = schema;
                this.documentExecuter = documentExecuter;
            }

            protected override async Task<ExecutionResult> HandleCore(GraphQuery query)
            {
                if (query == null) throw new ArgumentNullException(nameof(query));

                var result = await documentExecuter.ExecuteAsync(new ExecutionOptions
                {
                    Schema = this.schema,
                    Query = query.Query,
                    Inputs = query.Variables.ToInputs()
                }).ConfigureAwait(false);

                //if (result.Errors?.Count > 0) throw new BadRequestException(JsonConvert.SerializeObject(result));

                return result;
            }
        }
    }
}