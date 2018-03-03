using Commands.Events;
using Commands.ValidationHandlers;
using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.Validators
{
  public class CanOnlySaveQuestionsOnOwnQuestionnaire : IRule<CreateQuestion> {

    private IRead<Questionnaire> _questionnaireReader;

    public CanOnlySaveQuestionsOnOwnQuestionnaire(IRead<Questionnaire> readQuestionnaire) {
      _questionnaireReader = readQuestionnaire;
    }

    public string Message { get; set; }
    public Func<CreateQuestion, bool> IsValid { get { return IsOwnQuestionnaire; } set { } }

    public bool IsOwnQuestionnaire(CreateQuestion create) {
      var questionnaire = _questionnaireReader.GetById(create.QuestionnaireId);
      Message = "Questionnaire is not owned by user";
      return questionnaire.CreatedById == create.SavedById;
    }
  }
}
