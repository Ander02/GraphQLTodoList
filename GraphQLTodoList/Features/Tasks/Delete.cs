using FluentValidation;
using GraphQLTodoList.Features.Results;
using GraphQLTodoList.Infraestructure.Database;
using GraphQLTodoList.Infraestructure.Exceptions;
using GraphQLTodoList.Util.Extensions;
using MediatR;
using System;
using System.Threading.Tasks;

namespace GraphQLTodoList.Features.Tasks
{
    public class Delete
    {
        public class Command : IRequest<TaskResult.Full>
        {
            public Guid Id { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                //Validations
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

                if (task == null) throw new NotFoundException("The " + nameof(task) + " with Id: " + command.Id + " doesn't exist");

                if (!task.DeletedAt.IsDefaultDateTime()) throw new InvalidArgumentException("The " + nameof(task) + " has already been deleted");

                task.DeletedAt = DateTime.Now;
                await _db.SaveChangesAsync();

                return new TaskResult.Full(task);
            }
        }
    }
}
