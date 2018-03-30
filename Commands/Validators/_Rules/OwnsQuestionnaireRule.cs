using Commands.ValidationHandlers;
using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using Queries.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commands.Validators._Rules
{
  public abstract class OwnsQuestionnaireRule<T> : IRule<T> {
    private readonly IHub _hub;

    public virtual string Message { get; set; }
    public virtual Func<T, bool> IsValid { get; set; }

    public OwnsQuestionnaireRule(IHub hub) {
      _hub = hub;
      IsValid = Validate;
    }

    private bool Validate(T entity) {
      var questionnaireId = GetQuestionnaireId(entity);
      var userId = GetUserId(entity);

      var questionnaire = _hub.Send(new GetQuestionnaireById(questionnaireId));

      var ownsQuestionnaire = questionnaire.CreatedById == userId;

      if (!ownsQuestionnaire)
        Message = $"User[{userId}] cannot delete questionnaire";

      return ownsQuestionnaire;
    }

    public abstract int GetQuestionnaireId(T entity);
    public abstract int GetUserId(T entity);
  }
}
