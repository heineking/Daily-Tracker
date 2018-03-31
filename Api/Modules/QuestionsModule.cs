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
  public class QuestionsModule : NancyModule {

    public QuestionsModule(RouteHandlerFactory routeHandlerFactory) : base("/questions") {
      var handler = routeHandlerFactory.CreateRouteHandler(this);

      Get("/", _ => handler.Get<GetAllQuestions, List<QuestionModel>>(new GetAllQuestions()));
        
      Post("/", _ => {
        this.RequiresAuthentication();

        var currentUser = (DailyTrackerPrincipal)Context.CurrentUser;

        var createQuestion = this.Bind<CreateQuestion>();

        createQuestion.SetSavedById(currentUser.UserId);

        return handler.Post(createQuestion, createResponse);

        object createResponse(CreateQuestion created) {
          return new { id = created.QuestionId };
        }
      });

      Put("/{id:int}", _ => {
        this.RequiresAuthentication();

        var currentUser = (DailyTrackerPrincipal)Context.CurrentUser;

        var updateQuestion = this.Bind<UpdateQuestion>();

        updateQuestion.SetSavedById(currentUser.UserId);

        updateQuestion.QuestionId = _.id;

        return handler.Put(updateQuestion);

      });
    }
  }
}
