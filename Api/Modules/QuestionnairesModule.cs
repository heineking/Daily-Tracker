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
  public class QuestionnairesModule : NancyModule {
    public QuestionnairesModule(RouteHandlerFactory routeHandlerFactory) : base("/questionnaires") {
      var handler = routeHandlerFactory.CreateRouteHandler(this);

      Get("/", _ => handler.Get<GetAllQuestionnaires, List<QuestionnaireModel>>(new GetAllQuestionnaires()));
        
      Post("/", _ => {
        this.RequiresAuthentication();

        var currentUser = (DailyTrackerPrincipal)Context.CurrentUser;

        var createQuestionnaire = this.Bind<CreateQuestionnaire>();
        createQuestionnaire.QuestionnaireId = default(int);
        createQuestionnaire.SavedById = currentUser.UserId;

        return handler.Post(createQuestionnaire, createResponse);

        object createResponse(CreateQuestionnaire created) {
          return new { id = created.QuestionnaireId };
        }
      });

      Put("/{id:int}", _ => {
        this.RequiresAuthentication();

        var currentUser = (DailyTrackerPrincipal)Context.CurrentUser;

        var updateQuestionnaire = this.Bind<UpdateQuestionnaire>();

        updateQuestionnaire.QuestionnaireId = _.id;
        updateQuestionnaire.SavedById = currentUser.UserId;

        return handler.Put(updateQuestionnaire);

      });

      Delete("/{id:int}", _ => {
        this.RequiresAuthentication();

        var currentUser = (DailyTrackerPrincipal)Context.CurrentUser;

        var deleteQuestionnaire = new DeleteQuestionnaire {
          DeletedByUserId = currentUser.UserId,
          QuestionnaireId = _.id
        };

        return handler.Delete(deleteQuestionnaire);

      });
    }
  }
}
