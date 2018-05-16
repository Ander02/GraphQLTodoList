using FluentValidation.AspNetCore;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using GraphQLTodoList.Features;
using GraphQLTodoList.GraphQL.Root;
using GraphQLTodoList.GraphQL.Types;
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

            //GraphQL Types
            services.AddScoped<UserType>();
            services.AddScoped<TaskType>();

            //Users Input Type
            services.AddScoped<Features.Users.FindAll.InputType>();
            services.AddScoped<Features.Users.Register.InputType>();

            //Roots
            services.AddScoped<RootQuery>();
            services.AddScoped<RootMutation>();

            //Schema Config
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

            services.AddScoped<IDocumentExecuter, DocumentExecuter>();

            services.AddScoped<ISchema, GraphSchema>();
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