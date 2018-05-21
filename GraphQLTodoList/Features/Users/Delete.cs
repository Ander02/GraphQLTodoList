using FluentValidation;
using GraphQLTodoList.Features.Results;
using GraphQLTodoList.Infraestructure.Database;
using GraphQLTodoList.Infraestructure.Exceptions;
using GraphQLTodoList.Util.Extensions;
using MediatR;
using System;
using System.Threading.Tasks;

namespace GraphQLTodoList.Features.Users
{
    public class Delete
    {
        public class Command : IRequest<UserResult.Full>
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

                if (user == null) throw new NotFoundException("The user with Id: " + command.Id + " doesn't exist");

                if (!user.DeletedAt.IsDefaultDateTimeValue()) throw new InvalidArgumentException("The user has already been deleted");

                user.DeletedAt = DateTime.Now;
                await _db.SaveChangesAsync();

                return new UserResult.Full(user);
            }
        }
    }
}
