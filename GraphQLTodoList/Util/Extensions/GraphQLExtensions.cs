using FluentValidation.Results;
using GraphQL;
using GraphQL.Types;
using GraphQLTodoList.Infraestructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.Util.Extensions
{
    public static class GraphQLExtensions
    {
        public static void AddGraphQLExceptionRange(this ExecutionErrors errors, Exception exception)
        {
            switch (exception)
            {
                case InvalidArgumentException invalidArgumentEx:
                    errors.Add(new ExecutionError(invalidArgumentEx.Body));
                    break;

                case BaseException baseEx:
                    errors.AddRange(((List<ValidationFailure>)baseEx.Body).Select(validationFailure => new ExecutionError(validationFailure.ErrorMessage)));
                    break;

                default:
                    errors.Add(new ExecutionError(exception.Message));
                    break;
            }
        }

        public static async Task<object> TryResolveAsync(this ResolveFieldContext<object> context, Func<ResolveFieldContext<object>, Task<object>> resolve, Func<ExecutionErrors, Task<object>> error = null)
        {
            try
            {
                return await resolve(context);
            }
            catch (Exception ex)
            {
                if (error == null)
                {
                    context.Errors.AddGraphQLExceptionRange(ex);

                    return null;
                }
                return error(context.Errors);
            }
        }
    }
}
