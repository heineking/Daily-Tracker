using Mediator.Contracts;
using Queries.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Queries.Requests {
  public class GetQuestionByOptionId : IRequest<QuestionModel> {
    public int OptionId { get; }
    public GetQuestionByOptionId(int optionId) {
      OptionId = optionId;
    }
  }
}
