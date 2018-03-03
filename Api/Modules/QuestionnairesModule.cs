using Commands.Contracts;
using Commands.Events;
using Commands.ValidationHandlers;
using Commands.Validators;
using Mediator.Contracts;
using Nancy;
using Nancy.ModelBinding;
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
        var createQuestionnaire = this.Bind<CreateQuestionnaire>();

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
        var updateQuestionnaire = this.Bind<UpdateQuestionnaire>();
        updateQuestionnaire.QuestionnaireId = _.id;
        hub.Publish(updateQuestionnaire);
        return Negotiate.WithStatusCode(HttpStatusCode.Accepted);
      });
    }
  }
}
