using GraphQL.Types;
using GraphQLTodoList.Features.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.GraphQL.Types
{
    public class TaskType : ObjectGraphType<TaskResult.Full>
    {
        public TaskType()
        {
            Field<IdGraphType>(name: "Id", resolve: (context) => context.Source.Id);

            Field<StringGraphType>(name: "Name", resolve: (context) => context.Source.Name);
            Field<StringGraphType>(name: "Description", resolve: (context) => context.Source.Description);

            Field<DateGraphType>(name: "CreatedAt", resolve: (context) => context.Source.CreatedAt ?? default(DateTime));
            Field<DateGraphType>(name: "CompletedAt", resolve: (context) => context.Source.CompletedAt ?? default(DateTime));
            Field<DateGraphType>(name: "DeletedAt", resolve: (context) => context.Source.DeletedAt ?? default(DateTime));

            Field<UserType>(name: "User", resolve: (context) => context.Source.User);
        }
    }
}
