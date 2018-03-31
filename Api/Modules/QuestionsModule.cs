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

      Get("/", _ => handler.Get<GetAllQuestions, List<QuestionModel>>(() => new GetAllQuestions()));
        
      Post("/", _ => {
        this.RequiresAuthentication();
        return handler.Post(createRequest, createResponse);

        CreateQuestion createRequest() {
          var currentUser = (DailyTrackerPrincipal)Context.CurrentUser;
          var createQuestion = this.Bind<CreateQuestion>();
          createQuestion.SetSavedById(currentUser.UserId);
          return createQuestion;
        };

        object createResponse(CreateQuestion createQuestion) {
          return new { id = createQuestion.QuestionId };
        }
      });

      Put("/{id:int}", _ => {
        this.RequiresAuthentication();
        return handler.Put(createRequest);

        UpdateQuestion createRequest() {
          var currentUser = (DailyTrackerPrincipal)Context.CurrentUser;
          var updateQuestion = this.Bind<UpdateQuestion>();
          updateQuestion.SetSavedById(currentUser.UserId);
          updateQuestion.QuestionId = _.id;
          return updateQuestion;
        }
      });
    }
  }
}
