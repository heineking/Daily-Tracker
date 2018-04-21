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
  public class QuestionnairesModule : BaseModule {
    public QuestionnairesModule(RouteHandlerFactory routeHandlerFactory) : base("/questionnaires") {
      var handler = routeHandlerFactory.CreateRouteHandler(this);

      Get("/", _ => handler.Get<GetAllQuestionnaires, List<QuestionnaireModel>>(new GetAllQuestionnaires()));
        
      Post("/", _ => {
        this.RequiresAuthentication();
        
        var createQuestionnaire = this.Bind<CreateQuestionnaire>();
        createQuestionnaire.SavedById = User.UserId;

        return handler.Post(createQuestionnaire, createResponse);

        object createResponse(CreateQuestionnaire created) {
          return new { id = created.QuestionnaireId };
        }
      });

      Put("/{id:int}", _ => {
        this.RequiresAuthentication();
        var updateQuestionnaire = this.Bind<UpdateQuestionnaire>();

        updateQuestionnaire.QuestionnaireId = _.id;
        updateQuestionnaire.SavedById = User.UserId;

        return handler.Put(updateQuestionnaire);

      });

      Delete("/{id:int}", _ => {
        this.RequiresAuthentication();
        
        var deleteQuestionnaire = new DeleteQuestionnaire {
          DeletedById = User.UserId,
          QuestionnaireId = _.id
        };

        return handler.Delete(deleteQuestionnaire);

      });
    }
  }
}
