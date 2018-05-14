using FluentValidation;
using GraphQL;
using GraphQL.Types;
using GraphQLTodoList.Util.Extensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.GraphQL
{
    public class GraphQL
    {
        public class GQuery : IRequest<ExecutionResult>
        {
            public string OperationName { get; set; }
            public string NamedQuery { get; set; }
            public string Query { get; set; }
            public object Variables { get; set; }

            public override string ToString() => this.ToJson();
        }

        public class CommandValidator : AbstractValidator<GQuery>
        {
            public CommandValidator()
            {
                //Validations               
            }
        }

        public class Handler : AsyncRequestHandler<GQuery, ExecutionResult>
        {
            private readonly ISchema _schema;
            private readonly IDocumentExecuter _documentExecuter;

            public Handler(ISchema schema, IDocumentExecuter documentExecuter)
            {
                _schema = schema;
                _documentExecuter = documentExecuter;
            }

            protected override async Task<ExecutionResult> HandleCore(GQuery query)
            {
                var result = await _documentExecuter.ExecuteAsync(new ExecutionOptions
                {
                    Schema = _schema,
                    Query = query.Query,
                    Inputs = query.Variables != null ? query.Variables.ToString().ToInputs() : "".ToInputs()
                }).ConfigureAwait(false);

                //if (result.Errors?.Count > 0) throw new BadRequestException(JsonConvert.SerializeObject(result));

                return result;
            }
        }
    }
}