using Mediator.Contracts;
using Queries.Models;
using System.Collections.Generic;

namespace Queries.Requests {
  public class GetAllQuestionnaires : IRequest<List<QuestionnaireModel>> {
  }
}
