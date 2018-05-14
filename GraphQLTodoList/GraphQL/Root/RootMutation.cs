using GraphQL.Types;
using GraphQLTodoList.Features.Users;
using GraphQLTodoList.GraphQL.Types.InputTypes.Mutations;
using GraphQLTodoList.GraphQL.Types.OutputTypes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.GraphQL.Root
{
    public class RootMutation : ObjectGraphType
    {
        public RootMutation() { }


        public RootMutation(IMediator mediator)
        {
            #region Users

            //Register
            FieldAsync<UserType>(
                name: "RegisterUser",
                arguments: new QueryArguments
                {
                    new QueryArgument<RegisterUserInputType>()
                    {
                        Name = "input"
                    }
                },
                resolve: async (context) =>
                {
                    var input = context.GetArgument<Register.Command>("input");

                    var result = await mediator.Send(input);

                    return result;
                });

            #endregion
        }
    }
}
