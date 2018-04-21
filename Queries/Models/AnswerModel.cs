using System;
using System.Collections.Generic;
using System.Text;

namespace Queries.Models {
  public class AnswerModel {
    public int Id { get; set; }
    public int OptionId { get; set; }
    public int QuestionId { get; set; }
    public int UserId { get; set; }
    public DateTime AnswerDate { get; set; }
  }
}
