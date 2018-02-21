using Commands.Events;
using Mediator.Contracts;
using Nancy;
using Nancy.ModelBinding;
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
        var saveQuestion = this.Bind<SaveQuestion>();

        hub.Publish(saveQuestion);
        hub.Publish(new Commit());

        return Negotiate
          .WithStatusCode(HttpStatusCode.Created);

      });
    }
  }
}
