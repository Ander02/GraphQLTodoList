using FluentValidation;
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
    public class Elimine
    {
        public class Command : IRequest<bool>
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

        public class Handler : AsyncRequestHandler<Command, bool>
        {
            private readonly Db _db;

            public Handler(Db db)
            {
                _db = db;
            }

            protected override async Task<bool> HandleCore(Command command)
            {
                if (command == null) throw new InvalidArgumentException("The argument is null");

                var task = await _db.Tasks.FindAsync(command.Id);

                if (task == null) throw new NotFoundException("The " + nameof(task) + " with Id: " + command.Id + " doesn't exist");

                _db.Tasks.Remove(task);
                await _db.SaveChangesAsync();

                return true;
            }
        }
    }
}
