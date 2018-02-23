using System;

namespace DataAccessLayer.Contracts.Entities {
  public class Answer {
    public int AnswerId { get; set; }
    public int OptionId { get; set; }
    public int QuestionId { get; set; }
    public int UserId { get; set; }
    public DateTime AnswerDate { get; set; }
    public Question Question { get; set; }
    public Option Option { get; set; }
    public User User { get; set; }
  }
}