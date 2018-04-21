using Api.Auth;
using Api.Handlers;
using Commands.Events;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using Queries.Models;
using Queries.Requests;
using System.Collections.Generic;

namespace Api.Modules {
  public class QuestionsModule : BaseModule {

    public QuestionsModule(RouteHandlerFactory routeHandlerFactory) : base("/questions") {
      var handler = routeHandlerFactory.CreateRouteHandler(this);

      Get("/", _ => handler.Get<GetAllQuestions, List<QuestionModel>>(new GetAllQuestions()));
        
      Post("/", _ => {
        this.RequiresAuthentication();
        
        var createQuestion = this.Bind<CreateQuestion>();

        createQuestion.SavedById = User.UserId;

        return handler.Post(createQuestion, createResponse);

        object createResponse(CreateQuestion created) {
          return new { id = created.QuestionId };
        }
      });

      Put("/{id:int}", _ => {
        this.RequiresAuthentication();

        var updateQuestion = BindUpdateModel<IUpdateQuestion, UpdateQuestion>();

        updateQuestion.SavedById = User.UserId;
        updateQuestion.QuestionId = _.id;

        return handler.Put(updateQuestion);

      });
    }
  }
}
