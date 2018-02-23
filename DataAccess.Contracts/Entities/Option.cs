using System.Collections.Generic;

namespace DataAccessLayer.Contracts.Entities {
  public class Option {
    public int OptionId { get; set; }
    public int QuestionId { get; set; }
    public string OptionText { get; set; }
    public int OptionValue { get; set; }
    public Question Question { get; set; }
  }
}