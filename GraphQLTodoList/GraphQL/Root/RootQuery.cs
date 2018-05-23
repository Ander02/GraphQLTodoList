using GraphQL.Types;
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
            #region Hello
            Field<StringGraphType>(
                name: "Init",
                resolve: (context) => "Hello World GraphQL"
                );
            #endregion

            #region Users         
            //Find All
            FieldAsync<ListGraphType<UserType>>(
                name: "FindAllUsers",
                arguments: new QueryArguments
                {
                    new QueryArgument<Features.Users.FindAll.InputType>()
                    {
                        Name = "params"
                    }
                },
                resolve: async (context) => await context.TryResolveAsync(async (resolveContext) =>
                {
                    var query = context.GetArgument("params", new Features.Users.FindAll.Query());

                    var result = await mediator.Send(query);

                    return result;
                }));

            //Find By Id
            FieldAsync<UserType>(
                name: "FindUserById",
                arguments: new QueryArguments
                {
                    new QueryArgument<NonNullGraphType<IdGraphType>>()
                    {
                        Name = "Id"
                    }
                },
                resolve: async (context) => await context.TryResolveAsync(async (resolveContext) =>
                {
                    var query = new Features.Users.FindById.Query
                    {
                        Id = context.GetArgument<Guid>("Id")
                    };

                    var result = await mediator.Send(query);

                    return result;
                }));
            #endregion

            #region Tasks         
            //Find All
            FieldAsync<ListGraphType<TaskType>>(
                name: "FindAllTasks",
                arguments: new QueryArguments
                {
                    new QueryArgument<Features.Tasks.FindAll.InputType>()
                    {
                        Name = "params"
                    }
                },
                resolve: async (context) => await context.TryResolveAsync(async (resolveContext) =>
                {
                    var query = context.GetArgument("params", new Features.Tasks.FindAll.Query());

                    var result = await mediator.Send(query);

                    return result;
                }));

            //Find By Id
            FieldAsync<TaskType>(
                name: "FindTaskById",
                arguments: new QueryArguments
                {
                    new QueryArgument<NonNullGraphType<IdGraphType>>()
                    {
                        Name = "Id"
                    }
                },
                resolve: async (context) => await context.TryResolveAsync(async (resolveContext) =>
                {
                    var query = new Features.Tasks.FindById.Query
                    {
                        Id = context.GetArgument<Guid>("Id")
                    };

                    var result = await mediator.Send(query);

                    return result;
                }));
            #endregion
        }
    }
}
