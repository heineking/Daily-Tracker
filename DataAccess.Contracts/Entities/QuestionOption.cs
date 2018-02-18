namespace DataAccessLayer.Contracts.Entities {
  public class QuestionOption {
    public int QuestionOptionId { get; set; }
    public int OptionValue { get; set; }
    public Question Question { get; set; }
    public Option Option { get; set; }
  }
}