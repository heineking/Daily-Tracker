using Commands.Events;
using Commands.ValidationHandlers;
using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using Queries.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commands.Validators.QuestionnaireValidators
{
  public class QuestionnaireExistsRule : IRule<DeleteQuestionnaire> {
    private readonly IHub _hub;

    public string Message { get; set; }
    public Func<DeleteQuestionnaire, bool> IsValid { get; set; }

    public QuestionnaireExistsRule(IHub hub) {
      _hub = hub;
      Message = "Questionnaire does not exist";
      IsValid = Validate;
    }

    private bool Validate(DeleteQuestionnaire deleteQuestionnaire) {
      return _hub.Send(new GetQuestionnaireById(deleteQuestionnaire.QuestionnaireId)).QuestionnaireId > 0;
    }
  }
}
