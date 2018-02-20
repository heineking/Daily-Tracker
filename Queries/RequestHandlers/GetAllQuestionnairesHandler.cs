using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using Queries.Requests;
using System.Collections.Generic;
using System.Linq;

namespace Queries.RequestHandlers
{
  public class GetAllQuestionnairesHandler : IRequestHandler<GetAllQuestionnaires, List<Questionnaire>> {
    private readonly IRead<Questionnaire> _questionnaireReader;

    public GetAllQuestionnairesHandler(IRead<Questionnaire> questionnaireReader) {
      _questionnaireReader = questionnaireReader;
    }
    
    public List<Questionnaire> Handle(GetAllQuestionnaires request) {
      return _questionnaireReader.GetAll().ToList();
    }
  }
}
