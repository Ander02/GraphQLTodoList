using GraphQL;
using GraphQL.Instrumentation;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.GraphQL.Middleware
{
    public class FieldExceptionMiddleware
    {
        public Task<Object> Resolve(ResolveFieldContext context, FieldMiddlewareDelegate next)
        {
            try
            {
                return next(context);
            }
            catch (Exception e)
            {
                context.Errors.Add(new ExecutionError(e.Message));
                return null;
            }
        }
    }
}
