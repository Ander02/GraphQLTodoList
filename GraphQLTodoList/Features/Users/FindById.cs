﻿using FluentValidation;
using GraphQLTodoList.Features.Results;
using GraphQLTodoList.Infraestructure.Database;
using GraphQLTodoList.Infraestructure.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.Features.Users
{
    public class FindById
    {
        public class Query : IRequest<UserResult.Full>
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

        public class Handler : AsyncRequestHandler<Query, UserResult.Full>
        {
            private readonly Db _db;

            public Handler(Db db)
            {
                _db = db;
            }

            protected override async Task<UserResult.Full> HandleCore(Query query)
            {
                if (query == null) throw new InvalidArgumentException("The argument is null");

                var user = await _db.Users.Include(u => u.Tasks).Where(u => u.Id.Equals(query.Id)).FirstOrDefaultAsync();

                if (user == null) throw new NotFoundException("The " + nameof(user) + " with id: " + query.Id + " doesn't exist");

                return new UserResult.Full(user);
            }
        }
    }
}
