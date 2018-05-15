using GraphQL.Types;
using GraphQLTodoList.Features.Users;
using GraphQLTodoList.GraphQL.Types.InputTypes.Querys;
using GraphQLTodoList.GraphQL.Types.OutputTypes;
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
                    new QueryArgument<SearchUserInputType>()
                    {
                        Name = "params"
                    }
                },
                resolve: async (context) =>
                {
                    var input = context.GetArgument<FindAll.Query>("params");

                    var result = await mediator.Send(input);

                    throw new Exception("testee");

                    return result;
                });

            #endregion
        }
    }
}
