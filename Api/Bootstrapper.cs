
using DataAccess.Contracts.Persistance;
using DataAccess.Contracts.Repositories;
using DataAccess.Contracts.Strategies;
using DataAccessLayer.Contracts.Entities;
using DataAccessLayer.EntityFramework.Context;
using DataAccessLayer.EntityFramework.Persistence;
using DataAccessLayer.EntityFramework.Repositories;
using Mediator;
using Mediator.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.StructureMap;
using StructureMap;
using System.IO;

namespace Api {
  public class Bootstrapper : StructureMapNancyBootstrapper {
    protected override void ApplicationStartup(IContainer container, IPipelines pipelines) {
      base.ApplicationStartup(container, pipelines);
    }

    protected override void ConfigureApplicationContainer(IContainer existingContainer) {
      base.ConfigureApplicationContainer(existingContainer);

      // create the db context options
      var configuration = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json")
      .Build();

      var builder = new DbContextOptionsBuilder<DailyTrackerContext>();

      var connectionString = configuration.GetConnectionString("dailytracker");
      builder.UseSqlite(connectionString);

      existingContainer.Configure(cfg => {
        cfg.For<DbContextOptions>().Use(builder.Options);
      });

    }

    protected override void ConfigureRequestContainer(IContainer container, NancyContext context) {
      base.ConfigureRequestContainer(container, context);

      //var dbContextOptions = container.GetInstance(typeof(DbContextOptions<DailyTrackerContext>));

      container.Configure(cfg => {

        // scan for the handlers
        cfg.Scan(scanner => {
          scanner.Assembly("Services.Contracts");
          scanner.WithDefaultConventions();
          scanner.AddAllTypesOf(typeof(IRequestHandler<,>));
          scanner.AddAllTypesOf(typeof(IEventHandler<>));
        });

        // start context
        cfg.For<DbContext>().Use<DailyTrackerContext>();

        // strategies
        cfg.For<IEntityPredicate>().Use<EntityPredicate>();

        // register repos
        cfg.For<QuestionnaireRepository>().Use<QuestionnaireRepository>();
        cfg.For<IRead<Questionnaire>>().Use<QuestionnaireRepository>();
        cfg.For<IDelete<Questionnaire>>().Use<QuestionnaireRepository>();
        cfg.For<ISave<Questionnaire>>().Use<QuestionnaireRepository>();

        // unit of work
        cfg.For<IUnitOfWork>().Use<UnitOfWork>();

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
