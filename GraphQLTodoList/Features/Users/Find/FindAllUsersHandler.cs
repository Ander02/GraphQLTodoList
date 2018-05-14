using GraphQLTodoList.Features.Views;
using GraphQLTodoList.Infraestructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.Features.Users.Find
{
    public class FindAllUsersHandler : AsyncRequestHandler<FindAllUsersQuery, List<UserView>>
    {
        private readonly Db _db;

        public FindAllUsersHandler(Db db)
        {
            _db = db;
        }

        protected override async Task<List<UserView>> HandleCore(FindAllUsersQuery query)
        {
            if (query == null) { }

            var dbQuery = _db.Users.Include(u => u.Tasks).AsQueryable();

            if (!query.ShowDeleteds) dbQuery = dbQuery.Where(u => u.DeletedAt != DateTime.MinValue).AsQueryable();

            return (await dbQuery.ToListAsync()).Select(u => new UserView(u)).ToList();
        }
    }
}
