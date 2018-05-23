using GraphQLTodoList.Util.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphQLTodoList.Infraestructure.Exceptions
{
    public class InvalidArgumentException : BaseException
    {
        public InvalidArgumentException(string body) : base(400, body) { }
    }
}