using Mediator.Contracts;
using Queries.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Queries.Requests {
  public class GetOptionById : IRequest<OptionModel> {
    public int OptionId { get; }
    public GetOptionById(int optionId) {
      OptionId = optionId;
    }
  }
}
