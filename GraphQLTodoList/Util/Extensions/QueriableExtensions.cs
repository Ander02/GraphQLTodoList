using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.Util.Extensions
{
    public static class QueriableExtensions
    {
        public static IQueryable<object> SkipToPage(this IQueryable<object> query, int page, int pageLimit)
        {
            return query.Skip(pageLimit * page).Take(pageLimit);
        }
    }
}
