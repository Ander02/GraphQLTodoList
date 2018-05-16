using GraphQL.Types;
using GraphQLTodoList.Features.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.GraphQL.Types
{
    public class UserType : ObjectGraphType<UserResult.Full>
    {
        public UserType()
        {
            Field<IdGraphType>(name: "Id", resolve: (context) => context.Source.Id);

            Field<IntGraphType>(name: "Age", resolve: (context) => context.Source.Age ?? default(int));

            Field<StringGraphType>(name: "Name", resolve: (context) => context.Source.Name);
            Field<StringGraphType>(name: "Email", resolve: (context) => context.Source.Email);
            Field<StringGraphType>(name: "Password", resolve: (context) => context.Source.Password);

            Field<DateGraphType>(name: "CreatedAt", resolve: (context) => context.Source.CreatedAt ?? default(DateTime));

            Field<ListGraphType<TaskType>>(name: "Tasks", resolve: (context) => context.Source.Tasks);
        }
    }
}