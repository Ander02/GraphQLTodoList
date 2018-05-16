using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.Infraestructure.Exceptions
{
    public class BaseLogicException : Exception
    {
        public int ErrorCode { get; set; }
        public dynamic Body { get; set; }

        public BaseLogicException(int errorCode)
        {
            this.ErrorCode = errorCode;
        }

        public BaseLogicException(int errorCode, dynamic body) : this(errorCode)
        {
            this.Body = body;
        }
    }
}
