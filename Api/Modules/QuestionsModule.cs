using Api.Auth;
using Commands.Events;
using Commands.ValidationHandlers;
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

    public QuestionsModule(IHub hub, ValidatorFactory validatorFactory) : base("/questions") {
      
      Get("/", _ => {
        return Negotiate
          .WithStatusCode(HttpStatusCode.OK)
          .WithModel(hub.Send(new GetAllQuestions()));
      });

      Post("/", _ => {
        this.RequiresAuthentication();
        var currentUser = (DailyTrackerPrincipal)Context.CurrentUser;

        var createQuestion = this.Bind<CreateQuestion>();
        createQuestion.SetSavedById(currentUser.UserId);

        var validator = validatorFactory.CreateValidator<CreateQuestion>();
        var errors = validator.Validate(createQuestion);

        if (errors.Any())
          return Negotiate
            .WithStatusCode(HttpStatusCode.BadRequest)
            .WithModel(new { errors });

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
