using FluentValidation;
using FluentValidation.Results;
using GraphQL.Types;
using GraphQLTodoList.Features.Results;
using GraphQLTodoList.Infraestructure.Database;
using GraphQLTodoList.Infraestructure.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.Features.Tasks
{
    public class Register
    {
        public class Command : IRequest<TaskResult.Full>
        {
            public Guid UserId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class InputType : InputObjectGraphType<Command>
        {
            public InputType()
            {
                Name = "RegisterTaskInputType";

                Field<NonNullGraphType<IdGraphType>>(name: "UserId");
                Field<NonNullGraphType<StringGraphType>>(name: "Name");
                Field<NonNullGraphType<StringGraphType>>(name: "Description");
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(u => u.UserId).NotEmpty().NotNull();
                RuleFor(u => u.Name).NotEmpty().NotNull();
                RuleFor(u => u.Description).NotEmpty().NotNull();
            }
        }

        public class Handler : AsyncRequestHandler<Command, TaskResult.Full>
        {
            private readonly Db _db;

            public Handler(Db db)
            {
                _db = db;
            }

            protected override async Task<TaskResult.Full> HandleCore(Command command)
            {
                if (command == null) throw new InvalidArgumentException("The argument is null");

                var user = await _db.Users.FindAsync(command.UserId);

                if (user == null) throw new NotFoundException("The " + nameof(user) + " with id: " + command.UserId + " doesn't exist");

                var task = new Domain.Task()
                {
                    User = user,
                    Name = command.Name,
                    Description = command.Description,
                    CreatedAt = DateTime.Now
                };

                await _db.Tasks.AddAsync(task);
                await _db.SaveChangesAsync();

                return new TaskResult.Full(task);
            }
        }
    }
}
