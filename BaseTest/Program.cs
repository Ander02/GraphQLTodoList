using GraphQLTodoList.Util.Extensions;
using System;

namespace BaseTest
{
    class Program
    {
        public static void Main(String[] args)
        {
            var user = new GraphQLTodoList.Domain.User()
            {
                Age = 18,
                Name = "Anderson",
                CreatedAt = DateTime.Now
            };
            Console.WriteLine(user.ToJson());

            var upUser = new GraphQLTodoList.Features.Users.Update.Command()
            {
                Name = "John",
                Email = "teste@teste.com"
            };
            Console.WriteLine(upUser.ToJson());

            user.UpdatePropsByReflection(upUser);
            Console.WriteLine("Teste: ");
            Console.WriteLine(user.ToJson());

            Console.Read();
        }
    }
}
