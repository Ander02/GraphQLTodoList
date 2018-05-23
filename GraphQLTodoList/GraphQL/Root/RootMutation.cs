using GraphQL.Types;
using GraphQLTodoList.Features;
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
                    new QueryArgument<NonNullGraphType<Features.Users.Register.InputType>>()
                    {
                        Name = "input"
                    }
                },
                resolve: async (context) => await context.TryResolveAsync(async (resolveContext) =>
                {
                    var input = resolveContext.GetArgument<Features.Users.Register.Command>("input");

                    var result = await mediator.Send(input);

                    return result;
                }));

            //Update
            FieldAsync<UserType>(
                name: "UpdateUser",
                arguments: new QueryArguments
                {
                    new QueryArgument<NonNullGraphType<IdGraphType>>()
                    {
                        Name = "id"
                    },
                    new QueryArgument<NonNullGraphType<Features.Users.Update.InputType>>()
                    {
                        Name = "input"
                    }
                },
                resolve: async (context) => await context.TryResolveAsync(async (resolveContext) =>
                {
                    var id = resolveContext.GetArgument<Guid>("id");
                    var input = resolveContext.GetArgument<Features.Users.Update.Command>("input");

                    input.Id = id;

                    var result = await mediator.Send(input);

                    return result;
                }));

            //Delete
            FieldAsync<UserType>(
                name: "DeleteUser",
                arguments: new QueryArguments
                {
                    new QueryArgument<NonNullGraphType<IdGraphType>>()
                    {
                        Name = "id"
                    }
                },
                resolve: async (context) => await context.TryResolveAsync(async (resolveContext) =>
                {
                    var command = new Features.Users.Delete.Command()
                    {
                        Id = resolveContext.GetArgument<Guid>("id")
                    };

                    var result = await mediator.Send(command);

                    return result;
                }));

            //Remove
            FieldAsync<BooleanGraphType>(
                name: "RemoveUser",
                arguments: new QueryArguments
                {
                    new QueryArgument<NonNullGraphType<IdGraphType>>()
                    {
                        Name = "id"
                    }
                },
                resolve: async (context) => await context.TryResolveAsync(async (resolveContext) =>
                {
                    var command = new Features.Users.Elimine.Command()
                    {
                        Id = resolveContext.GetArgument<Guid>("id")
                    };

                    var result = await mediator.Send(command);

                    return result;
                }));

            #endregion

            #region Tasks

            //Register
            FieldAsync<TaskType>(
                name: "RegisterTask",
                arguments: new QueryArguments
                {
                    new QueryArgument<NonNullGraphType<Features.Tasks.Register.InputType>>()
                    {
                        Name = "input"
                    }
                },
                resolve: async (context) => await context.TryResolveAsync(async (resolveContext) =>
                {
                    var input = resolveContext.GetArgument<Features.Tasks.Register.Command>("input");

                    var result = await mediator.Send(input);

                    return result;
                }));

            //Update
            FieldAsync<TaskType>(
                name: "UpdateTask",
                arguments: new QueryArguments
                {
                    new QueryArgument<NonNullGraphType<IdGraphType>>()
                    {
                        Name = "id"
                    },
                    new QueryArgument<NonNullGraphType<Features.Tasks.Update.InputType>>()
                    {
                        Name = "input"
                    }
                },
                resolve: async (context) => await context.TryResolveAsync(async (resolveContext) =>
                {
                    var id = resolveContext.GetArgument<Guid>("id");
                    var input = resolveContext.GetArgument<Features.Tasks.Update.Command>("input");

                    input.Id = id;
                    var result = await mediator.Send(input);

                    return result;
                }));

            //Delete
            FieldAsync<TaskType>(
                name: "DeleteTask",
                arguments: new QueryArguments
                {
                    new QueryArgument<NonNullGraphType<IdGraphType>>()
                    {
                        Name = "id"
                    }
                },
                resolve: async (context) => await context.TryResolveAsync(async (resolveContext) =>
                {
                    var command = new Features.Tasks.Delete.Command()
                    {
                        Id = resolveContext.GetArgument<Guid>("id")
                    };

                    var result = await mediator.Send(command);

                    return result;
                }));

            //Remove
            FieldAsync<BooleanGraphType>(
                name: "RemoveTask",
                arguments: new QueryArguments
                {
                    new QueryArgument<NonNullGraphType<IdGraphType>>()
                    {
                        Name = "id"
                    }
                },
                resolve: async (context) => await context.TryResolveAsync(async (resolveContext) =>
                {
                    var command = new Features.Tasks.Elimine.Command()
                    {
                        Id = resolveContext.GetArgument<Guid>("id")
                    };

                    var result = await mediator.Send(command);

                    return result;
                }));

            #endregion
        }
    }
}
