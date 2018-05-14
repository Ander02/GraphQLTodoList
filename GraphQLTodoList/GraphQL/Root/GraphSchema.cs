using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.GraphQL.Root
{
    public class GraphSchema : Schema
    {
        public GraphSchema(Func<Type, GraphType> resolve) : base(resolve)
        {
            //Roots
            Query = (RootQuery) resolve(typeof(RootQuery));
            Mutation = (RootMutation) resolve(typeof(RootMutation));
        }
    }
}
