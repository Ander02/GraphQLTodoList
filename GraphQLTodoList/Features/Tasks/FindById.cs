using FluentValidation;
using GraphQLTodoList.Features.Results;
using GraphQLTodoList.Infraestructure.Database;
using GraphQLTodoList.Infraestructure.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.Features.Tasks
{
    public class FindById
    {
        public class Query : IRequest<TaskResult.Full>
        {
            public Guid Id { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                //Query Validations
            }
        }

        public class Handler : AsyncRequestHandler<Query, TaskResult.Full>
        {
            private readonly Db _db;

            public Handler(Db db)
            {
                _db = db;
            }

            protected override async Task<TaskResult.Full> HandleCore(Query query)
            {
                if (query == null) throw new InvalidArgumentException("The argument is null");

                var task = await _db.Tasks.Include(t => t.User).Where(t => t.Id.Equals(query.Id)).FirstOrDefaultAsync();

                if (task == null) throw new NotFoundException("The " + nameof(task) + " with id: " + query.Id + " doesn't exist");

                return new TaskResult.Full(task);
            }
        }
    }
}
