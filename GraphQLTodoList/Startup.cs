using FluentValidation.AspNetCore;
using GraphQL;
using GraphQL.Types;
using GraphQLTodoList.GraphQL.Root;
using GraphQLTodoList.GraphQL.Types.InputTypes.Mutations;
using GraphQLTodoList.GraphQL.Types.InputTypes.Querys;
using GraphQLTodoList.GraphQL.Types.OutputTypes;
using GraphQLTodoList.Infraestructure.Database;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLTodoList
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Application Config

            services.AddMvc()
            //.AddFeatureFolders()
            .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>())
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            });

            services.AddCors();
            services.AddMediatR(typeof(Startup));
            #endregion

            #region Database Config

            services.AddDbContext<Db>((options) =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion

            #region FluentValidator Config

            #endregion

            #region GraphQL Config

            //Output Types
            services.AddTransient<UserType>();
            services.AddTransient<TaskType>();

            //Querys Input Type
            services.AddTransient<SearchUserInputType>();

            //Mutations Input Type
            services.AddTransient<RegisterUserInputType>();

            //Roots
            services.AddScoped<RootQuery>();
            services.AddScoped<RootMutation>();

            //Schema Config
            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            var servicesProvider = services.BuildServiceProvider();
            services.AddScoped<ISchema>(schema => new GraphSchema(type => (GraphType)servicesProvider.GetService(type))
            {
                Query = servicesProvider.GetService<RootQuery>(),
                Mutation = servicesProvider.GetService<RootMutation>()
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            #region Development Config
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseGraphiQl();
            }
            #endregion

            #region Cors Config
            app.UseCors(builder => builder.AllowAnyOrigin().WithMethods(new string[] { "GET", "POST", "PUT", "PATCH", "DELETE", "OPTIONS" }).AllowAnyHeader());
            #endregion

            #region Middlewares Config

            #endregion

            app.UseMvc();
        }
    }
}