using System;
using System.Collections.Generic;

namespace DataAccessLayer.Contracts.Entities {
  public class Result {
    public int ResultId { get; set; }
    public int UserId { get; set; }
    public int QuestionnaireId { get; set; }

    public DateTime TakenDate { get; set; }
    public string AdditionalNotes { get; set; }

    public ICollection<Answer> Answers { get; set; }
    public Questionnaire Questionnaire { get; set; }
    public User User { get; set; }
  }
}