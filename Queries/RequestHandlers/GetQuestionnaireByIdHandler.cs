using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using Queries.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Queries.RequestHandlers
{
  public class GetQuestionnaireByIdHandler : IRequestHandler<GetQuestionnaireById, Questionnaire> {
    private readonly IRead<Questionnaire> _questionnaireReader;

    public GetQuestionnaireByIdHandler(IRead<Questionnaire> questionnaireReader) {
      _questionnaireReader = questionnaireReader;
    }

    public Questionnaire Handle(GetQuestionnaireById request) {
      // TODO: not sure if null object pattern is appropriate here...?
      return _questionnaireReader.GetById(request.QuestionnaireId) ?? new Questionnaire { };
    }
  }
}
