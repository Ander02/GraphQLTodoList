using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.Util.Extensions
{
    public static class ObjectExtensions
    {
        public static bool NotEquals(this object obj1, object obj2) => !obj1.Equals(obj2);
    }
}
