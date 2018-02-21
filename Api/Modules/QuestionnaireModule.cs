using Commands.Events;
using Mediator.Contracts;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Validation;
using Queries.Requests;

namespace Api.Modules {
  public class QuestionnaireModule : NancyModule {
    public QuestionnaireModule(IHub hub) : base("/questionnaires") {
      Get("/", _ => {
        return Negotiate
          .WithStatusCode(HttpStatusCode.OK)
          .WithModel(hub.Send(new GetAllQuestionnaires()));
      });

      Post("/", _ => {
        var saveQuestionnaire = this.Bind<SaveQuestionnaire>();

        hub.Publish(saveQuestionnaire);
        hub.Publish(new Commit());

        return Negotiate.WithStatusCode(HttpStatusCode.Created);
      });
    }
  }
}
