using System.Collections.Generic;
using System.Reflection;

namespace GraphQLTodoList.Util.Comparators
{
    public class EqualityComparerPropertyInfoName : IEqualityComparer<PropertyInfo>
    {
        public bool Equals(PropertyInfo x, PropertyInfo y)
        {
            return x.Name.Equals(y.Name);
        }

        public int GetHashCode(PropertyInfo obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
