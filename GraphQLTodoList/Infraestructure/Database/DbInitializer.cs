using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.Infraestructure.Database
{
    public static class DbInitializer
    {
        public static async Task Initialize(Db db)
        {
            await db.SaveChangesAsync();
        }
    }
}
