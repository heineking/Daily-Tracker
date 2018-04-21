using Mediator.Contracts;
using Queries.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Queries.Requests {
  public class GetAnswersByUserId : IRequest<List<AnswerModel>> {
    public int UserId { get; }
    public GetAnswersByUserId(int userId) {
      UserId = userId;
    }
  }
}
