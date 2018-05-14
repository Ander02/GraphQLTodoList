﻿using GraphQL.Types;
using GraphQLTodoList.Features.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.GraphQL.Types.OutputTypes
{
    public class TaskType : ObjectGraphType<TaskResult>
    {
        public TaskType()
        {
            Field<IdGraphType>(name: "Id", resolve: (context) => context.Source.Id);
            Field<StringGraphType>(name: "Name", resolve: (context) => context.Source.Name);
        }
    }
}