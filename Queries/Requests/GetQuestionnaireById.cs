using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Queries.Requests {
  public class GetQuestionnaireById : IRequest<Questionnaire> {
    public int QuestionnaireId { get; }
    public GetQuestionnaireById(int questionnaireId) {
      QuestionnaireId = questionnaireId;
    }
  }
}
