namespace Queries.Models {
  public class OptionModel {
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public string OptionText { get; set; }
    public int OptionValue { get; set; }
  }
}