﻿using GraphQL.Types;
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
                    var query = context.GetArgument("params", new FindAll.Query());

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
                    var query = new FindById.Query
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
