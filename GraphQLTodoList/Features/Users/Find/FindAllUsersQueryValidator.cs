using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.Features.Users.Find
{
    public class FindAllUsersQueryValidator : AbstractValidator<FindAllUsersQuery>
    {
        public FindAllUsersQueryValidator()
        {
            //Query Validations
        }
    }
}
