using System.Collections.Generic;

namespace DataAccessLayer.Contracts.Entities {
  public class Option {
    public int OptionId { get; set; }
    public string OptionText { get; set; }
    public ICollection<QuestionOption> QuestionOptions { get; set; }
  }
}