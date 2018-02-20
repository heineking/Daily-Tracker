using Commands.Events;
using Mediator.Contracts;
using Nancy;
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

        hub.Publish(new SaveQuestionnaire {
          Description = "New Questionnaire",
          Name = "Questionnaire"
        });

        hub.Publish(new Commit());

        return Negotiate
          .WithStatusCode(HttpStatusCode.Created);
      });
    }
  }
}
