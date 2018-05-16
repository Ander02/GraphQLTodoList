using FluentValidation;
using FluentValidation.Results;
using GraphQL.Types;
using GraphQLTodoList.Domain;
using GraphQLTodoList.Features.Results;
using GraphQLTodoList.Infraestructure.Database;
using GraphQLTodoList.Infraestructure.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.Features.Users
{
    public class Register
    {
        public class Command : IRequest<UserResult.Full>
        {
            public string Name { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public int Age { get; set; }
        }

        public class InputType : InputObjectGraphType<Command>
        {
            public InputType()
            {
                Name = "RegisterUserInputType";

                Field<NonNullGraphType<IntGraphType>>(name: "Age");
                Field<NonNullGraphType<StringGraphType>>(name: "Name");
                Field<NonNullGraphType<StringGraphType>>(name: "Email");
                Field<NonNullGraphType<StringGraphType>>(name: "Password");
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(u => u.Name).NotEmpty().NotNull();
                RuleFor(u => u.Email).NotEmpty().NotNull().EmailAddress();
                RuleFor(u => u.Password).NotEmpty().NotNull().MinimumLength(8);
                RuleFor(u => u.Age).NotEmpty().NotNull().GreaterThan(0);
            }
        }

        public class Handler : AsyncRequestHandler<Command, UserResult.Full>
        {
            private readonly Db _db;

            public Handler(Db db)
            {
                _db = db;
            }

            protected override async Task<UserResult.Full> HandleCore(Command command)
            {
                if (command == null) throw new InvalidArgumentException("The argument is null");

                var user = new User()
                {
                    Age = command.Age,
                    Name = command.Name,
                    Email = command.Email,
                    Password = command.Password,
                    CreatedAt = DateTime.Now
                };

                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();

                return new UserResult.Full(user);
            }
        }
    }
}
