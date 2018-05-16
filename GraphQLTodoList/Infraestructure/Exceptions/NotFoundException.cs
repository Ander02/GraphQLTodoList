using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.Infraestructure.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string body) : base(404, body) { }
    }
}
