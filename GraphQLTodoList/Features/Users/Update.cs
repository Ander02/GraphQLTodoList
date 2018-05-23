using FluentValidation;
using GraphQL.Types;
using GraphQLTodoList.Domain;
using GraphQLTodoList.Features.Results;
using GraphQLTodoList.Infraestructure.Database;
using GraphQLTodoList.Infraestructure.Exceptions;
using GraphQLTodoList.Util.Extensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.Features.Users
{
    public class Update
    {
        public class Command : IRequest<UserResult.Full>
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public int? Age { get; set; }
        }

        public class InputType : InputObjectGraphType<Command>
        {
            public InputType()
            {
                Name = "UpdateUserInputType";
                Field<IntGraphType>(name: "Age");
                Field<StringGraphType>(name: "Name");
                Field<StringGraphType>(name: "Email");
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(u => u.Id).NotEmpty().NotNull();
                RuleFor(u => u.Name).NotEmpty().NotNull();
                RuleFor(u => u.Email).NotEmpty().NotNull().EmailAddress();
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

                var user = await _db.Users.FindAsync(command.Id);

                if (user == null) throw new NotFoundException("User with id: " + command.Id + " doesn't exist");

                //user.Name = command.Name ?? user.Name;
                //user.Email = command.Email ?? user.Email;
                //user.Age = command.Age ?? user.Age;

                user.UpdatePropsByReflection(command);

                await _db.SaveChangesAsync();

                return new UserResult.Full(user);
            }
        }
    }
}
