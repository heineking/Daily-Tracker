using Mediator.Contracts;
using Queries.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Queries.Requests {
  public class GetAllOptions : IRequest<List<OptionModel>> {
  }
}
