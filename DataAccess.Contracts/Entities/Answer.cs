namespace DataAccessLayer.Contracts.Entities {
  public class Answer {
    public int AnswerId { get; set; }
    public int ResultId { get; set; }
    public string AdditionalNotes { get; set; }
    public Result Result { get; set; }
  }
}