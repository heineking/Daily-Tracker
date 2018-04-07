
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
using Serilog.Filters;
using Infrastructure.Profiling;
using System.Diagnostics;
using Api.Settings;
using Security.Contracts.Hashing;
using Security;
using Commands.ValidationHandlers;
using Security.JWT;
using Security.Contracts.JWT;
using JWT.Algorithms;
using JWT;
using JWT.Serializers;
using Nancy.Authentication.Stateless;
using System;
using Api.Wrappers;
using Api.Auth;
using Api.Handlers;
using Infrastructure.Hashing;
using Queries.Caching;

namespace Api {
  public class Bootstrapper : StructureMapNancyBootstrapper {
    private readonly IServiceCollection _services;

    public Bootstrapper(IServiceCollection services) {
      _services = services;
    }

    protected override void ApplicationStartup(IContainer container, IPipelines pipelines) {
      base.ApplicationStartup(container, pipelines);
      var jwtService = container.GetInstance<IJWTService>();

      var configuration = new StatelessAuthenticationConfiguration(ctx => {
        try {
          var bearerDeclaration = "Bearer ";
          var authHeader = ctx.Request.Headers.Authorization;
          var jwt = authHeader.Substring(bearerDeclaration.Length);
          var token = jwtService.DecodeToken(jwt);

          if (token != null)
            return new DailyTrackerPrincipal(token.UserId, token.Username);

        } catch (Exception) {
          return null;
        }
        return null;
      });

      StatelessAuthentication.Enable(pipelines, configuration);
    }

    protected override void ConfigureApplicationContainer(IContainer existingContainer) {
      //base.ConfigureApplicationContainer(existingContainer);

      // setup our logger for the whole application
      // TODO: move to extension?
      var logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .Enrich.WithExceptionDetails()
        .WriteTo.Logger(l =>
          l.Filter
          .ByIncludingOnly(Matching.WithProperty("Elapsed"))
          .WriteTo.RollingFile("logs\\Perf-{Date}.log")
        )
        .WriteTo.Logger(l =>
          l.Filter
            .ByExcluding(Matching.WithProperty("Elapsed"))
            .WriteTo.RollingFile("logs\\dailytracker-{Date}.log")
        )
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
        // transfer over the startup services for nancy DI to use
        cfg.Populate(_services);

        // scan for the handlers
        cfg.Scan(scanner => {
          scanner.Assembly("Commands");
          scanner.Assembly("Queries");
          scanner.WithDefaultConventions();
          scanner.AddAllTypesOf(typeof(IRequestHandler<,>));
          scanner.AddAllTypesOf(typeof(IEventHandler<>));
          scanner.AddAllTypesOf(typeof(IRule<>));
          scanner.AddAllTypesOf(typeof(AbstractValidator<>));
        });

        // register the db context options
        cfg.For<DbContextOptions>().Use(builder.Options);
        
        // register our serilog provider
        cfg.For<ILogger>().Use(logger);
        cfg.For<IStopwatch>().Use(ctx => new StopwatchAdapter(new Stopwatch()));

        // register our proxy factories
        cfg.For<LoggerProxyFactory>().Use<LoggerProxyFactory>();
        cfg.For<TimerProxyFactory>().Use<TimerProxyFactory>();
        cfg.For<CacheProxyFactory>().Use<CacheProxyFactory>();

        // register hashing classes
        cfg.For<IHashSettings>().Use<HashSettings>();
        cfg.For<IHasherFactory>().Use<HasherFactory>();
        cfg.For<IHasher>().Use(ctx => ctx.GetInstance<IHasherFactory>().Create());

        // register the token classes
        cfg.For<IJWTSettings>().Use<JWTSettings>();
        cfg.For<ITokenSettings>().Use<TokenSettings>();
        cfg.For<IJWTService>().Use<JWTService>();

        cfg.For<IJwtAlgorithm>().Use<HMACSHA256Algorithm>();
        cfg.For<IJwtEncoder>().Use<JwtEncoder>();
        cfg.For<IJwtDecoder>().Use<JwtDecoder>();
        cfg.For<IDateTimeProvider>().Use<UtcDateTimeProvider>();
        cfg.For<IJwtValidator>().Use<JwtValidator>();
        cfg.For<IJsonSerializer>().Use<JsonNetSerializer>();
        cfg.For<IBase64UrlEncoder>().Use<JwtBase64UrlEncoder>();

      });


    }

    protected override void ConfigureRequestContainer(IContainer container, NancyContext context) {
      base.ConfigureRequestContainer(container, context);

      container.Configure(cfg => {



        // start context
        cfg.For<DbContext>().Use<DailyTrackerContext>();

        // strategies
        cfg.For<IEntityPredicate>().Use<EntityPredicate>();

        // hasher
        cfg.For<IHash>().Use<MD5HashAdapter>();

        // register repos
        var loggerProxyFactory = container.GetInstance<LoggerProxyFactory>();
        var timerProxyFactory = container.GetInstance<TimerProxyFactory>();

        // questionnaires
        cfg.For<IRead<Questionnaire>>().Use<QuestionnaireRepository>().DecorateWith(q => loggerProxyFactory.Create(timerProxyFactory.Create(q)));
        cfg.For<IDelete<Questionnaire>>().Use<QuestionnaireRepository>().DecorateWith(q => loggerProxyFactory.Create(timerProxyFactory.Create(q)));
        cfg.For<ISave<Questionnaire>>().Use<QuestionnaireRepository>().DecorateWith(q => loggerProxyFactory.Create(timerProxyFactory.Create(q)));

        // questions
        cfg.For<IRead<Question>>().Use<QuestionRepository>().DecorateWith(q => loggerProxyFactory.Create(timerProxyFactory.Create(q)));
        cfg.For<IDelete<Question>>().Use<QuestionRepository>().DecorateWith(q => loggerProxyFactory.Create(timerProxyFactory.Create(q)));
        cfg.For<ISave<Question>>().Use<QuestionRepository>().DecorateWith(q => loggerProxyFactory.Create(timerProxyFactory.Create(q)));

        // users
        cfg.For<IRead<User>>().Use<UserRepository>().DecorateWith(u => loggerProxyFactory.Create(timerProxyFactory.Create(u)));
        cfg.For<IDelete<User>>().Use<UserRepository>().DecorateWith(u => loggerProxyFactory.Create(timerProxyFactory.Create(u)));
        cfg.For<ISave<User>>().Use<UserRepository>().DecorateWith(u => loggerProxyFactory.Create(timerProxyFactory.Create(u)));

        // user directory
        cfg.For<IRead<UserDirectory>>().Use<UserDirectoryRepository>().DecorateWith(u => loggerProxyFactory.Create(timerProxyFactory.Create(u)));
        cfg.For<ISave<UserDirectory>>().Use<UserDirectoryRepository>().DecorateWith(u => loggerProxyFactory.Create(timerProxyFactory.Create(u)));
        cfg.For<IDelete<UserDirectory>>().Use<UserDirectoryRepository>().DecorateWith(u => loggerProxyFactory.Create(timerProxyFactory.Create(u)));

        // unit of work
        cfg.For<IUnitOfWork>().Use<UnitOfWork>().DecorateWith(q => loggerProxyFactory.Create(timerProxyFactory.Create(q))); ;

        // mediator
        cfg.For<IHub>().Use<Hub>();

        // route handling
        cfg.For<RouteHandlerFactory>().Use<RouteHandlerFactory>();

        var cacheProxyFactory = container.GetInstance<CacheProxyFactory>();

        cfg.For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => cacheProxyFactory.Create(FactoryMethods.Execute(() => ctx.GetInstance(t))));
        cfg.For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));

        // jwt
        cfg.For<ITokenGenerator>().Use<TokenGenerator>();

      });

    }

    protected override void RequestStartup(IContainer container, IPipelines pipelines, NancyContext context) {
      base.RequestStartup(container, pipelines, context);
    }
  }
}
