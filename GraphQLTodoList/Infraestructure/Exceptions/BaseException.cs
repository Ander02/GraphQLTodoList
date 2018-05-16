using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.Infraestructure.Exceptions
{
    public class BaseException : Exception
    {
        public int ErrorCode { get; set; }
        public dynamic Body { get; set; }

        public BaseException(int errorCode)
        {
            this.ErrorCode = errorCode;
        }

        public BaseException(int errorCode, dynamic body) : this(errorCode)
        {
            this.Body = body;
        }
    }
}
