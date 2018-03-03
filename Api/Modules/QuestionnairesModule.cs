using Api.Auth;
using Commands.Contracts;
using Commands.Events;
using Commands.ValidationHandlers;
using Commands.Validators;
using Mediator.Contracts;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using Nancy.Validation;
using Queries.Requests;
using System;
using System.Linq;

namespace Api.Modules {
  public class QuestionnairesModule : NancyModule {
    public QuestionnairesModule(IHub hub, ValidatorFactory validatorFactory) : base("/questionnaires") {
      Get("/", _ => {
        return Negotiate
          .WithStatusCode(HttpStatusCode.OK)
          .WithModel(hub.Send(new GetAllQuestionnaires()));
      });

      Post("/", _ => {
        this.RequiresAuthentication();
        var currentUser = (DailyTrackerPrincipal)Context.CurrentUser;

        var createQuestionnaire = this.Bind<CreateQuestionnaire>();
        createQuestionnaire.SetSavedById(currentUser.UserId);

        var validator = validatorFactory.CreateValidator<CreateQuestionnaire>();
        var errors = validator.Validate(createQuestionnaire).ToList();

        if (!errors.Any()) {
          hub.Publish(createQuestionnaire);
          return Negotiate
            .WithStatusCode(HttpStatusCode.Created)
            .WithModel(new { id = createQuestionnaire.QuestionnaireId });
        }

        return Negotiate
          .WithStatusCode(HttpStatusCode.BadRequest)
          .WithModel(new { errors });

      });

      Put("/{id:int}", _ => {
        this.RequiresAuthentication();
        var currentUser = (DailyTrackerPrincipal)Context.CurrentUser;

        var updateQuestionnaire = this.Bind<UpdateQuestionnaire>();
        updateQuestionnaire.QuestionnaireId = _.id;
        updateQuestionnaire.SetSavedById(currentUser.UserId);

        var validator = validatorFactory.CreateValidator<UpdateQuestionnaire>();
        var errors = validator.Validate(updateQuestionnaire);

        if (errors.Any())
          return Negotiate
            .WithStatusCode(HttpStatusCode.BadRequest)
            .WithModel(new { errors });

        updateQuestionnaire.QuestionnaireId = _.id;
        hub.Publish(updateQuestionnaire);
        return Negotiate.WithStatusCode(HttpStatusCode.Accepted);
      });
    }
  }
}
