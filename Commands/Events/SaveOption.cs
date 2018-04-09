using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.Events {
  public abstract class SaveOption {
    public int OptionId { get; set; }
    public int QuestionId { get; set; }
    public string Text { get; set; }
    public int Value { get; set; }
    public int SavedById { get; set; }
  }
}
