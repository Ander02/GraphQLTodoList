using GraphQLTodoList.Domain;
using GraphQLTodoList.Features.Views;
using GraphQLTodoList.Infraestructure.Database;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.Features.Users.Register
{
    public class SearchManyUsersHandler : AsyncRequestHandler<SearchManyUsersQuery, UserView>
    {
        private readonly Db _db;

        public SearchManyUsersHandler(Db db)
        {
            _db = db;
        }

        protected override async Task<UserView> HandleCore(SearchManyUsersQuery command)
        {
            var user = new User()
            {
                Age = command.Age,
                Name = command.Name,
                Email = command.Email,
                Password = command.Password,
                CreatedAt = DateTime.Now
            };

            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            return new UserView(user);
        }
    }
}
