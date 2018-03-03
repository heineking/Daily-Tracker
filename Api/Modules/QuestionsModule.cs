using Api.Auth;
using Commands.Events;
using Mediator.Contracts;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using Queries.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Modules {
  public class QuestionsModule : NancyModule {

    public QuestionsModule(IHub hub) : base("/questions") {
      
      Get("/", _ => {
        return Negotiate
          .WithStatusCode(HttpStatusCode.OK)
          .WithModel(hub.Send(new GetAllQuestions()));
      });

      Post("/", _ => {
        this.RequiresAuthentication();
        var currentUser = (DailyTrackerPrincipal)Context.CurrentUser;

        var createQuestion = this.Bind<CreateQuestion>();
        createQuestion.SetQuestionId(currentUser.UserId);

        hub.Publish(createQuestion);
        return Negotiate
          .WithStatusCode(HttpStatusCode.Created)
          .WithModel(new { id = createQuestion.QuestionId });
      });

      Put("/{id:int}", _ => {
        this.RequiresAuthentication();
        var currentUser = (DailyTrackerPrincipal)Context.CurrentUser;

        var updateQuestion = this.Bind<UpdateQuestion>();
        updateQuestion.SetSavedById(currentUser.UserId);
        updateQuestion.QuestionId = _.id;

        hub.Publish(updateQuestion);
        return Negotiate
          .WithStatusCode(HttpStatusCode.Accepted);
      });
    }
  }
}
