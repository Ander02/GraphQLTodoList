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
                    new QueryArgument<NonNullGraphType<Register.InputType>>()
                    {
                        Name = "input"
                    }
                },
                resolve: async (context) => await context.TryResolveAsync(async (resolveContext) =>
                {
                    var input = resolveContext.GetArgument<Register.Command>("input");

                    var result = await mediator.Send(input);

                    return result;
                }));

            //Delete User
            FieldAsync<UserType>(
                name: "DeleteUser",
                arguments: new QueryArguments
                {
                    new QueryArgument<NonNullGraphType<IdGraphType>>()
                    {
                        Name = "Id"
                    }
                },
                resolve: async (context) => await context.TryResolveAsync(async (resolveContext) =>
                {
                    var command = new Delete.Command()
                    {
                        Id = resolveContext.GetArgument<Guid>("Id")
                    };
                    
                    var result = await mediator.Send(command);

                    return result;
                }));

            #endregion
        }
    }
}
