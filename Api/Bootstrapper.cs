
using DataAccess.Contracts.Persistance;
using DataAccess.Contracts.Repositories;
using DataAccess.Contracts.Strategies;
using DataAccessLayer.Contracts.Entities;
using DataAccessLayer.EntityFramework.Context;
using DataAccessLayer.EntityFramework.Persistence;
using DataAccessLayer.EntityFramework.Repositories;
using Mediator;
using Mediator.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.StructureMap;
using StructureMap;
using System.IO;
using DataAccess.EntityFramework.Repositories;
using Infrastructure.Proxies;
using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Json;

namespace Api {
  public class Bootstrapper : StructureMapNancyBootstrapper {
    private readonly IServiceCollection _services;
    
    public Bootstrapper(IServiceCollection services) {
      _services = services;
    }

    protected override void ApplicationStartup(IContainer container, IPipelines pipelines) {
      base.ApplicationStartup(container, pipelines);
    }

    protected override void ConfigureApplicationContainer(IContainer existingContainer) {
      base.ConfigureApplicationContainer(existingContainer);

      // setup our logger for the whole application
      var logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .Enrich.WithExceptionDetails()
        .WriteTo.RollingFile(
          new JsonFormatter(renderMessage: true),
          "logs\\dailytracker-{Date}.log")
        .CreateLogger();
          
      // TODO: move this to start up?
      // create the db context options
      var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

      var builder = new DbContextOptionsBuilder<DailyTrackerContext>();
      var connectionString = configuration.GetConnectionString("dailytracker");
      builder.UseSqlite(connectionString);

      existingContainer.Configure(cfg => {
        // transfer over the startup services
        cfg.Populate(_services);

        // register the db context options
        cfg.For<DbContextOptions>().Use(builder.Options);

        // register our serilog provider
        cfg.For<ILogger>().Use(logger);

        // register our proxy factories
        cfg.For<LoggerProxyFactory>().Use<LoggerProxyFactory>();

      });

    }

    protected override void ConfigureRequestContainer(IContainer container, NancyContext context) {
      base.ConfigureRequestContainer(container, context);
      
      container.Configure(cfg => {

        // scan for the handlers
        cfg.Scan(scanner => {
          scanner.Assembly("Commands");
          scanner.Assembly("Queries");
          scanner.WithDefaultConventions();
          scanner.AddAllTypesOf(typeof(IRequestHandler<,>));
          scanner.AddAllTypesOf(typeof(IEventHandler<>));
        });

        // start context
        cfg.For<DbContext>().Use<DailyTrackerContext>();

        // strategies
        cfg.For<IEntityPredicate>().Use<EntityPredicate>();

        var loggerProxyFactory = container.GetInstance<LoggerProxyFactory>();

        // register repos

        // questionnaire
        cfg.For<QuestionnaireRepository>().Use<QuestionnaireRepository>();
        cfg.For<IRead<Questionnaire>>().Use(ctx => loggerProxyFactory.Create<IRead<Questionnaire>>(ctx.GetInstance<QuestionnaireRepository>()));
        cfg.For<IDelete<Questionnaire>>().Use(ctx => loggerProxyFactory.Create<IDelete<Questionnaire>>(ctx.GetInstance<QuestionnaireRepository>()));
        cfg.For<ISave<Questionnaire>>().Use(ctx => loggerProxyFactory.Create<ISave<Questionnaire>>(ctx.GetInstance<QuestionnaireRepository>()));

        // questions
        cfg.For<QuestionRepository>().Use<QuestionRepository>();
        cfg.For<IRead<Question>>().Use<QuestionRepository>();
        cfg.For<IDelete<Question>>().Use<QuestionRepository>();
        cfg.For<ISave<Question>>().Use<QuestionRepository>();

        // unit of work
        cfg.For<UnitOfWork>().Use<UnitOfWork>();
        cfg.For<IUnitOfWork>().Use(ctx => loggerProxyFactory.Create<IUnitOfWork>(ctx.GetInstance<UnitOfWork>()));

        // mediator
        cfg.For<IHub>().Use<Hub>();
        cfg.For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
        cfg.For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));
      });
      
    }

    protected override void RequestStartup(IContainer container, IPipelines pipelines, NancyContext context) {
      base.RequestStartup(container, pipelines, context);
    }
  }
}
