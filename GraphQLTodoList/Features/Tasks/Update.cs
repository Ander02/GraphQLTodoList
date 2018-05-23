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

namespace GraphQLTodoList.Features.Tasks
{
    public class Update
    {
        public class Command : IRequest<TaskResult.Full>
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class InputType : InputObjectGraphType<Command>
        {
            public InputType()
            {
                Name = "UpdateTaskInputType";
                Field<StringGraphType>(name: "Name");
                Field<StringGraphType>(name: "Description");
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(t => t.Name).NotEmpty().NotNull();
                RuleFor(t => t.Description).NotEmpty().NotNull();
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

                var task = await _db.Tasks.FindAsync(command.Id);

                if (task == null) throw new NotFoundException("The " + nameof(task) + " with id: " + command.Id + " doesn't exist");

                //task.Name = command.Name ?? task.Name;
                //task.Description = command.Description ?? task.Description;

                task.UpdatePropsByReflection(command);

                await _db.SaveChangesAsync();

                return new TaskResult.Full(task);
            }
        }
    }
}
