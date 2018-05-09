using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using GraphQLTodoList.Features.Root;
using GraphQLTodoList.Infraestructure.Database;
using GraphQLTodoList.Util.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLTodoList
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Application Config

            services.AddMvc((options) =>
            {
                //options.Filters.Add(typeof(ValidationActionFilter));
            })
            //.AddFeatureFolders()
            //.AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>())
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

            #region GraphQL Config

            #region Types


            #endregion

            #region Mutation Input TYpes


            #endregion

            #region Querys Input Type


            #endregion

            #region Root

            services.AddScoped<RootQuery>();
            services.AddScoped<RootMutation>();
            #endregion

            #region Schema Config

            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            var servicesProvider = services.BuildServiceProvider();
            services.AddScoped<ISchema>(schema => new RootSchema(type => (GraphType)servicesProvider.GetService(type))
            {
                Query = servicesProvider.GetService<RootQuery>(),
                Mutation = servicesProvider.GetService<RootMutation>()
            });
            #endregion

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Cors Config
            app.UseCors(builder => builder.AllowAnyOrigin().WithMethods(new string[] { "GET", "POST", "PUT", "PATCH", "DELETE", "OPTIONS" }).AllowAnyHeader());
            #endregion

            #region Middlewares Config

            #endregion

            app.UseMvc();
        }
    }
}