using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.Events {
  public class DeleteQuestion {
    public int QuestionId { get; set; }
    public int DeletedById { get; set; }
  }
}
