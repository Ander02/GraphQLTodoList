using FluentValidation;
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

                var user = await _db.Users.FindAsync(command.Id);

                if (user == null) throw new NotFoundException("The user with Id: " + command.Id + " doesn't exist");

                _db.Users.Remove(user);
                await _db.SaveChangesAsync();

                return true;
            }
        }
    }

}
