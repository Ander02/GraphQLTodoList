using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.Features.Users.Register
{
    public class SearchManyUsersQueryValidator : AbstractValidator<SearchManyUsersQuery>
    {
        public SearchManyUsersQueryValidator()
        {
            RuleFor(u => u.Name).NotEmpty().NotNull();
            RuleFor(u => u.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(u => u.Password).NotEmpty().NotNull().MinimumLength(8);
            RuleFor(u => u.Age).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}