using GraphQL.Types;
using GraphQLTodoList.Features.Users;
using GraphQLTodoList.GraphQL.Types;
using GraphQLTodoList.Util.Extensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.GraphQL.Root
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery(IMediator mediator)
        {
            #region Users
            Field<StringGraphType>(name: "Hello", resolve: (context) => "Hello GraphQL");
            //Find All
            FieldAsync<ListGraphType<UserType>>(
                name: "FindAllUsers",
                arguments: new QueryArguments
                {
                    new QueryArgument<FindAll.InputType>()
                    {
                        Name = "params"
                    }
                },
                resolve: async (context) => await context.TryResolveAsync(async (resolveContext) =>
                {
                    var input = context.GetArgument<FindAll.Query>("params");

                    var result = await mediator.Send(input);

                    return result;
                }));

            #endregion
        }
    }
}
