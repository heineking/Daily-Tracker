using Commands.Contracts;
using Commands.Events;
using Commands.Validators;
using Mediator.Contracts;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Validation;
using Queries.Requests;
using System;

namespace Api.Modules {
  public class QuestionnairesModule : NancyModule {
    public QuestionnairesModule(IHub hub, IValidatorHandler validatorHandler) : base("/questionnaires") {
      Get("/", _ => {
        return Negotiate
          .WithStatusCode(HttpStatusCode.OK)
          .WithModel(hub.Send(new GetAllQuestionnaires()));
      });

      Post("/", _ => {
        var createQuestionnaire = this.Bind<CreateQuestionnaire>();
        var (isValid, errors) = validatorHandler.IsValid<CreateQuestionnaireValidator, CreateQuestionnaire>(createQuestionnaire);

        if (!isValid)
          return Negotiate
            .WithStatusCode(HttpStatusCode.BadRequest)
            .WithModel(new { errors });

        hub.Publish(createQuestionnaire);
        return Negotiate
          .WithStatusCode(HttpStatusCode.Created)
          .WithModel(new { id = createQuestionnaire.QuestionnaireId });
      });

      Put("/{id:int}", _ => {
        var updateQuestionnaire = this.Bind<UpdateQuestionnaire>();
        updateQuestionnaire.QuestionnaireId = _.id;
        hub.Publish(updateQuestionnaire);
        return Negotiate.WithStatusCode(HttpStatusCode.Accepted);
      });
    }
  }
}
