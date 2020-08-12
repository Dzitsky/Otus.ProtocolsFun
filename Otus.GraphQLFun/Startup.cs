using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Otus.GraphQLFun
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
      services.AddControllers();
      services
        .AddGraphQL(sp =>
        {
          var schemaBuilder = SchemaBuilder
            .New()
            .AddAuthorizeDirectiveType()
            .AddQueryType<Query>()
            // .AddMutationType<Mutation>()
            .AddType<CharacterType>()
            .AddType<CharacterResolvers>();
            // .AddType<MutationType>()
            // .AddType<AlertTemplateType>()
            // .AddType<IndicatorInputType>()
            // .AddType<MonitoredRicType>()
            // .AddType<IndicatorResolvers>();

          return schemaBuilder.Create();
        });
      
      services.AddSingleton<ICharacterRepository, CharacterRepository>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();
      
      app
        .UseWebSockets()
        .UseGraphQL("/graphql")
        .UseGraphiQL("/graphql")
        .UsePlayground("/graphql")
        .UseVoyager("/graphql");

      app.UseAuthorization();

      app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
  }
}