using GraphQLTodoList;
using GraphQLTodoList.Infraestructure.Database;
using GraphQLTodoList.Util.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool runSeed = false;

            if (args.Contains("seed"))
            {
                runSeed = true;
                args = args.Where(d => d != "seed").ToArray();
            }

            var host = BuildWebHost(args);

            //Execute Seed
            if (runSeed) RunSeed(host).Wait();

            //Execute normally
            else host.Run();
        }

        private static async Task RunSeed(IWebHost host)
        {
            Console.WriteLine("Running seed...");
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var services = scope.ServiceProvider;
                    var db = services.GetService<Db>();

                    await DbInitializer.Initialize(db);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetBaseException().Message);
                }

                Console.WriteLine("Seed ended");
                Console.Read();
            }
        }

        public static IWebHost BuildWebHost(string[] args) => WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build();
    }
}
