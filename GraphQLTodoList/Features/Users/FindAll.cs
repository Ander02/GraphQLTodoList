﻿using FluentValidation;
using GraphQL.Types;
using GraphQLTodoList.Features.Results;
using GraphQLTodoList.Infraestructure.Database;
using GraphQLTodoList.Infraestructure.Exceptions;
using GraphQLTodoList.Util.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.Features.Users
{
    public class FindAll
    {
        public class Query : BaseFindManyQuery, IRequest<List<UserResult.Full>>
        {

        }

        public class InputType : InputObjectGraphType<Query>
        {
            public InputType()
            {
                Name = "FindAllUsersInputType";

                Field<BooleanGraphType>(name: "ShowDeleteds");
                Field<IntGraphType>(name: "Limit");
                Field<IntGraphType>(name: "Page");
            }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                //Query Validations
            }
        }

        public class Handler : AsyncRequestHandler<Query, List<UserResult.Full>>
        {
            private readonly Db _db;

            public Handler(Db db)
            {
                _db = db;
            }

            protected override async Task<List<UserResult.Full>> HandleCore(Query query)
            {
                if (query == null) throw new InvalidArgumentException("The argument is null");

                var dbQuery = _db.Users.Include(u => u.Tasks).AsQueryable();

                if (!query.ShowDeleteds) dbQuery = dbQuery.Where(u => u.DeletedAt.IsDefaultDateTime()).AsQueryable();

                dbQuery = dbQuery.OrderBy(u => u.Name);
                dbQuery = dbQuery.Skip(query.Page * query.Limit).Take(query.Limit);

                return (await dbQuery.ToListAsync()).Select(u => new UserResult.Full(u)).ToList();
            }
        }
    }
}
