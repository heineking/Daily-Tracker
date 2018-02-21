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
        var createQuestion = this.Bind<CreateQuestion>();
        hub.Publish(createQuestion);
        return Negotiate
          .WithStatusCode(HttpStatusCode.Created)
          .WithModel(new { id = createQuestion.QuestionId });
      });

      Put("/{id:int}", _ => {
        var updateQuestion = this.Bind<UpdateQuestion>();
        updateQuestion.QuestionId = _.id;
        hub.Publish(updateQuestion);
        return Negotiate
          .WithStatusCode(HttpStatusCode.Accepted);
      });
    }
  }
}
