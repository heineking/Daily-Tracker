using System;
using System.Collections.Generic;
using System.Text;

namespace Queries.Models {
  public class QuestionOptionModel {
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public int OptionId { get; set; }
  }
}
