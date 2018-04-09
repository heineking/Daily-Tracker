using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.Events {
  public class UpdateAnswer {
    public int AnswerId { get; set; }
    public int OptionId { get; set; }
    public int QuestionId { get; set; }
    public int UserId { get; set; }
  }
}
