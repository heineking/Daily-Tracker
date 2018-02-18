using System.Collections.Generic;

namespace DataAccessLayer.Contracts.Entities {
  public class Question {
    public int QuestionId { get; set; }
    public string QuestionText { get; set; }
    public ICollection<QuestionOption> QuestionOptions { get; set; }
  }
}