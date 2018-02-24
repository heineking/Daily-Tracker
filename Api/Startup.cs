using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nancy.Owin;
using AutoMapper;
using Infrastructure.Mapper;
using System.Reflection;

namespace Api {
  public class Startup {
    private IServiceCollection Services;

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services) {
      services.AddAutoMapper(typeof(DomainProfile).GetTypeInfo().Assembly);

      Services = services;
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {

      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      }

      app.UseOwin(builder => builder.UseNancy(ctx => ctx.Bootstrapper = new Bootstrapper(Services)));
    }
  }
}
