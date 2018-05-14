using GraphQLTodoList.Features.Views;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.Features.Users.Find
{
    public class FindAllUsersQuery : IRequest<List<UserView>>
    {
        public bool ShowDeleteds { get; set; } = false;
        public int Limit { get; set; } = 100;
        public int Page { get; set; } = 0;
    }
}