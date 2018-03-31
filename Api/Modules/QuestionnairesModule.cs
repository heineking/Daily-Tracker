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

      Get("/", _ => handler.Get<GetAllQuestionnaires, List<QuestionnaireModel>>(() => new GetAllQuestionnaires());
        
      Post("/", _ => {
        this.RequiresAuthentication();
        return handler.Post(createRequest, createResponse);

        CreateQuestionnaire createRequest() {
          var currentUser = (DailyTrackerPrincipal)Context.CurrentUser;
          var createQuestionnaire = this.Bind<CreateQuestionnaire>();
          createQuestionnaire.SetSavedById(currentUser.UserId);
          return createQuestionnaire;
        }

        object createResponse(CreateQuestionnaire createQuestionnaire) {
          return new { id = createQuestionnaire.QuestionnaireId };
        }
      });

      Put("/{id:int}", _ => {
        this.RequiresAuthentication();
        return handler.Put(createRequest);

        UpdateQuestionnaire createRequest() {
          var currentUser = (DailyTrackerPrincipal)Context.CurrentUser;
          var updateQuestionnaire = this.Bind<UpdateQuestionnaire>();
          updateQuestionnaire.QuestionnaireId = _.id;
          updateQuestionnaire.SetSavedById(currentUser.UserId);
          return updateQuestionnaire;
        }
      });

      Delete("/{id:int}", _ => {
        this.RequiresAuthentication();
        return handler.Delete(CreateRequest);

        DeleteQuestionnaire CreateRequest() {
          var currentUser = (DailyTrackerPrincipal)Context.CurrentUser;
          var deleteQuestionnaire = new DeleteQuestionnaire {
            DeletedByUserId = currentUser.UserId,
            QuestionnaireId = _.id
          };
          return deleteQuestionnaire;
        }
      });
    }
  }
}
