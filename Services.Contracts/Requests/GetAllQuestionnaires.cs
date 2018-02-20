using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Contracts.Requests {
  public class GetAllQuestionnaires : IRequest<List<Questionnaire>> {
  }
}
