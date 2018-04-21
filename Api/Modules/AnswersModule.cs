using Api.Handlers;
using Commands.Events;
using Nancy.ModelBinding;
using Nancy.Security;
using Queries.Models;
using Queries.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Modules {
  public class AnswersModule : BaseModule {
    public AnswersModule(RouteHandlerFactory routeHandlerFactory) : base("answers") {
      var handler = routeHandlerFactory.CreateRouteHandler(this);

      Get("/", _ => {
        this.RequiresAuthentication();
        return handler.Get<GetAnswersByUserId, List<AnswerModel>>(new GetAnswersByUserId(User.UserId));
      });

      Post("/", _ => {
        this.RequiresAuthentication();

        var createAnswer = this.Bind<CreateAnswer>();
        createAnswer.UserId = User.UserId;

        return handler.Post(createAnswer, created => new { id = created.AnswerId });
      });

      Put("/{id:int}", _ => {
        this.RequiresAuthentication();

        var updateAnswer = BindUpdateModel<IUpdateAnswer, UpdateAnswer>();

        updateAnswer.UserId = User.UserId;
        updateAnswer.AnswerId = _.id;

        return handler.Put(updateAnswer);
      });
    }
  }
}
