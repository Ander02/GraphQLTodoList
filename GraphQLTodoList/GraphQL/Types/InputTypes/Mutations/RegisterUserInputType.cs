using GraphQL.Types;
using GraphQLTodoList.Features.Results;
using GraphQLTodoList.Features.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.GraphQL.Types.InputTypes.Mutations
{
    public class RegisterUserInputType : InputObjectGraphType<Register.Command>
    {
        public RegisterUserInputType()
        {
            Field<NonNullGraphType<IntGraphType>>(name: "Age");
            Field<NonNullGraphType<StringGraphType>>(name: "Name");
            Field<NonNullGraphType<StringGraphType>>(name: "Email");
            Field<NonNullGraphType<StringGraphType>>(name: "Password");
        }
    }
}
