using GraphQL.Types;

namespace GraphQLTodoList.GraphQL.Types.InputTypes.Querys
{
    public class SearchUserInputType : InputObjectGraphType
    {
        public SearchUserInputType()
        {
            Field<BooleanGraphType>(name: "ShowDeleteds");
            Field<IntGraphType>(name: "Limit");
            Field<IntGraphType>(name: "Page");
        }
    }
}