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
    public static class GraphQLExecutionErrorsExtensions
    {
        public static void AddGraphQLExceptionRange(this ExecutionErrors errors, Exception ex)
        {
            switch (ex)
            {
                case BaseLogicException br:
                    errors.AddRange(((List<ValidationFailure>)br.Body).Select(validationFailure => new ExecutionError(validationFailure.ErrorMessage)));
                    break;
                default:
                    errors.Add(new ExecutionError(ex.Message));
                    break;
            }
        }
    }

    public static class GraphQLExtensions
    {
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
