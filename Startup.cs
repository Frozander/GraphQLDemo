using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatboxDemo.Data;
using ChatboxDemo.GraphQL;
using ChatboxDemo.GraphQL.Messages;
using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace gql_demo
{
  public class Startup
  {
    private readonly IConfiguration Configuration;

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors();

      services.AddPooledDbContextFactory<ChatboxDbContext>(opt => opt.UseSqlServer(
          Configuration.GetConnectionString("ChatboxConStr")
      ));

      services
          .AddGraphQLServer()
          .AddQueryType<Query>()
          .AddMutationType<Mutation>()
          .AddSubscriptionType<Subscription>()
          .AddType<MessageType>()
          .AddType<AddMessageInputType>()
          .AddType<AddMessagePayloadType>()
          .AddFiltering()
          .AddSorting()
          .AddInMemorySubscriptions();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseCors(
          options => options
                          .AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod()
      );

      app.UseWebSockets();

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapGraphQL();
      });

      app.UseGraphQLVoyager(new VoyagerOptions
      {
        GraphQLEndPoint = "/graphql"
      }, path: "/graphql-voyager");
    }
  }
}
